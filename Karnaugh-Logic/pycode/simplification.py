import re, sys
import numpy as np

'''
print('input an equation, using +, ･, /')
string = input()
'''


string = 'a + b ･ c + d'        #入力を文字列とする．本来はinputで外部から受付  evalで文に変換，実行
string_trans = string.translate(string.maketrans({'･': 'and', '+': 'or', '/': 'not '}))
#string_trans = 'a and b or b'

print('Bool_func = ', end='')
print(string_trans)

#変数の抽出
regex = re.compile(r'\w+')
regex = regex.findall(string)      #各変数名のリスト

var = []
for x in regex:
    if x not in var:
        var.append(x)
#var = ['a', 'b']

table = []

#真理値表を作る関数
def output(table, string_trans, var, value=[0]*len(var), n=0, count=0):

        num = len(var)
        n = int(n % num)
        for i in range(num):
                exec(var[i] + '=value[i]')

        for i in [0, 1]:

                exec(var[n] + '=i')              #例えば，a = 1
                value[n] = i

                #最後の要素に値を代入している時，string_transを実行，tableに格納
                if n == num - 1:
                    exec('table.append(eval(string_trans))')
                else:
                    output(table, string_trans, var, value, n+1, count+1)

output(table, string_trans, var)


#真理値表の出力を2次元行列にする
#niはカルノー図の行数，njは列数
num = len(var)
if num <= 0:
    print('Eoor: the number of variables must be greater than zero.')
elif num <= 2:
    nx = 2
    ny = int(2**num / nx)
elif num >= 3:
    nx = 4
    ny = int(2**num / nx)

    if num == 5:
        nz = 2
    elif num == 6:
        nz = 4

table = np.reshape(table, (nx, ny))


e = np.eye(4)
elem = np.eye(4)
elem[2, :] = e[3, :]
elem[3, :] = e[2, :]

if num <= 2:
    karn = table
elif num == 3:
    karn = np.dot(elem, table)
elif num == 4:
    karn = np.dot(np.dot(elem, table), elem)
elif num >= 5:
    print("Error: This programme doesn't support over 5 variables.")
    sys.exit()


#サークル発見のための総乗関数
def product(i, j, ni, nj, matrix):
    result = 1
    for x in range(ni + 1):
        for y in range(nj + 1):
            result *= matrix[i - x, j - y]
    return result

class Circle():
    def __init__(self, sizex, sizey):
        self.circle = np.zeros((nx, ny))
        self.sizex = sizex
        self.sizey = sizey
    
    #サークルの基準セル(右下)を記憶するリスト
    def detect_circle(self, karn):
        for i in range(nx):
            for j in range(ny):
                self.circle[i, j] = product(i, j, self.sizex-1, self.sizey-1, karn)

    #サークルの重複度を記憶するリスト
    def detect_dup(self, dup, i=0, j=0):
        if self.circle[i, j]:
            for x in range(self.sizex):
                for y in range(self.sizey):
                    dup[i - x, j - y] += 1
        if j < ny - 1:
            self.detect_dup(dup, i, j+1)
        elif i != nx - 1 and j == ny - 1:
            self.detect_dup(dup, i+1, 0)
        else:
            return

class Duplication():
    def __init__(self, dup):
        self.dup = dup

    def deletion(self, circleinst, i=0, j=0):
        sizex = circleinst.sizex
        sizey = circleinst.sizey
        flag = False
        if circleinst.circle[i, j]:
            for x in range(sizex):
                for y in range(sizey):
                    if self.dup[i - x, j - y] < 2:
                        flag = True
                        break
                if flag:
                    break
            else:
                print(sizex, sizey)
                for x in range(sizex):
                    for y in range(sizey):
                        self.dup[i - x, j - y] -= 1
                circleinst.circle[i, j] -= 1    #ループをすべて突破したら該当サークルを除外

        if j < ny - 1:
            self.deletion(circleinst, i, j+1)
        elif i != nx - 1 and j == ny - 1:
            self.deletion(circleinst, i+1, 0)
        else:
            return

dup = np.zeros((nx, ny))

size12 = Circle(1, 2)
size21 = Circle(2, 1)
size22 = Circle(2, 2)
size14 = Circle(1, 4)
size41 = Circle(4, 1)
size24 = Circle(2, 4)
size42 = Circle(4, 2)

size12.detect_circle(karn)
size21.detect_circle(karn)
size22.detect_circle(karn)
size14.detect_circle(karn)
size41.detect_circle(karn)
size24.detect_circle(karn)
size42.detect_circle(karn)

size12.detect_dup(dup)
size21.detect_dup(dup)
size22.detect_dup(dup)
size41.detect_dup(dup)
size14.detect_dup(dup)
size24.detect_dup(dup)
size42.detect_dup(dup)

duplication = Duplication(dup)
duplication.deletion(size12)
duplication.deletion(size21)
duplication.deletion(size22)
duplication.deletion(size14)
duplication.deletion(size41)
duplication.deletion(size24)
duplication.deletion(size42)







#printing karnaugh fig.
# for i in range(nx):
#     for j in range(ny):
#         print(int(karn[i, j]), end='\t')
#     print()

#簡単化された論理式を出力するため
#カルノー図各セルにおける，各変数の符号を格納するsignリスト作成
temp = np.zeros((num, 2**num))
ls = np.empty(num)

def substitute(temp, var, ls, n=0, count=[]):

    num = len(var)

    for i in [-1, 1]:
        ls[n] = i
        if(n < num - 1):
            substitute(temp, var, ls, n+1)

        if(n == num - 1):
            # print(ls, index)
            count.append([])
            temp[:, len(count) - 1] = ls[:]

substitute(temp, var, ls)
sign = np.empty((num, nx, ny))

for n in range(num):
    sign[n] = temp[n].reshape(nx, ny)

for n in range(num):
    if num <= 2:
        sign[n] = temp[n]
    elif num == 3:
        sign[n] = np.dot(elem, sign[n])
    elif num == 4:
        sign[n] = np.dot(np.dot(elem, sign[n]), elem)
