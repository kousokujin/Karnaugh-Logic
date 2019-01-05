import re
import numpy as np
import sys

'''
print('input an equation, using +, ･, /')
string = input()
'''


string = 'a + b'        #入力を文字列とする．本来はinputで外部から受付  evalで文に変換，実行
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
    print('Error: Not supported over 5 variables.')
    sys.exit()


#サークル発見のための総乗関数
def product(i, j, ni, nj, matrix, result = 1):
    for x in range(i - ni, i + 1):
        for y in range(j - nj, j + 1):
            result *= matrix[x, y]
    return result

#論理式出力の際に使う？カルノー図の各点における各変数の符号をリストで保存しておく
#coord[variable_name，row_num，column_num]
if num == 1:

    coord = np.empty((2, 1))
    coord[0, 0] = -1
    coord[1, 0] = 1

elif num == 2:

    coord = np.empty((2, 2, 2))

    coord[0, 0, :] = -1
    coord[0, 1, :] = 1

    coord[1, :, 0] = -1
    coord[1, :, 1] = 1

elif num == 3:

    coord = np.empty((3, 4, 2))

    coord[0, 0, :] = -1
    coord[0, 1, :] = 1
    coord[0, 2, :] = -1
    coord[0, 3, :] = 1
    
    coord[1, 0, :] = -1
    coord[1, 1, :] = 1
    coord[1, 2, :] = 1
    coord[1, 3, :] = -1

    coord[2, :, 0] = -1
    coord[2, :, 1] = 1

elif num == 4:

    coord = np.empty((4, 4, 4))

    coord[0, 0, :] = -1
    coord[0, 1, :] = 1
    coord[0, 2, :] = -1
    coord[0, 3, :] = 1

    coord[1, 0, :] = -1
    coord[1, 1, :] = 1
    coord[1, 2, :] = 1
    coord[1, 3, :] = -1

    coord[2, :, 0] = -1
    coord[2, :, 1] = -1
    coord[2, :, 2] = 1
    coord[2, :, 3] = 1

    coord[3, :, 0] = -1
    coord[3, :, 1] = 1
    coord[3, :, 2] = 1
    coord[3, :, 3] = -1

elif num >= 5:
    print('Error: Not supported over 5 variables.')
    sys.exit()

#各サイズのサークルが成立したかを記憶する
#[0] = 1x2, [1] = 2x1, [2] = 2x2, [3] = 1x4, [4] = 4x1, [5] = 2x4, [6] = 4x2

circle = np.zeros((7, nx, ny))

# if product(0, 0, -3, -3, karn):
#     print('Bool_func = 1')
#     sys.exit()

for i in range(nx):
    for j in range(ny):
        if num >= 3:
            circle[6, i, j] = product(i, j, 3, 1, karn)    #4x2
        if num >= 4:
            circle[5, i, j] = product(i, j, 1, 3, karn)    #2x4
        if num >= 3 and not circle[6, i, j]:
            circle[4, i, j] = product(i, j, 3, 0, karn)     #4x1
        if num >= 4 and not circle[5, i, j]:
            circle[3, i, j] = product(i, j, 0, 3, karn)     #1x4
        if num >= 2 and not circle[6, i, j] and not circle[5, i, j]:
            circle[2, i, j] = product(i, j, 1, 1, karn)    #2x2
        
        if not circle[6, i, j] and not circle[5, i, j] and not circle[2, i, j]:

            #境界(右端，下端)の場合，添字を修正
            if i == nx - 1:
                X = 1
            else:
                X = i
            if j == ny - 1:
                Y = 1
            else:
                Y = j

            if not circle[4, i, j] and not (karn[i - 1, Y] and karn[i, Y]):
                circle[1, i, j] = product(i, j, - 1, 0, karn)     #2x1
            
            if num >= 2 and not circle[3, i, j] and not (karn[X, j - 1] and karn[X, j]):
                circle[0, i, j] = product(i, j, 0, -1, karn)     #1x2


#重複を消去する関数
def dupl(i, j, karn, circle):
    if karn[i - 2, j - 1] * karn[i - 1, j - 1] * karn[i - 1, j] * karn[i, j]:
        circle[1, i - 1, j] = 0
    if karn[i, j - 1] * karn[i - 1, j - 1] * karn[i - 1, j] * karn[i - 2, j]:
        circle[1, i - 1, j] = 0
    if karn[i - 1, j - 2] * karn[i - 1, j - 1] * karn[i, j - 1] * karn[i, j]:
        circle[i, j - 1] = 0
    if karn[i, j - 2] * karn[i, j - 1] * karn[i - 1, j - 1] * karn[i - 1, j]:
        circle[i, j - 1] = 0


for i in range(nx):
    for j in range(ny):
        dupl(i, j, karn, circle)
