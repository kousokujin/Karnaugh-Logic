import re
import numpy as np

'''
print('input an equation, using +, ･, !')
string = input()
'''


string = 'a ･ b + b'        #入力を文字列とする．本来はinputで外部から受付  evalで文に変換，実行
string_trans = string.translate(string.maketrans({'･': 'and', '+': 'or', '!': 'not '}))
#string_trans = 'a and b or b'


#変数の抽出
regex = re.compile(r'\w+')
regex = regex.findall(string)      #各変数名のリスト

var = []
for x in regex:
    if x not in var:
        var.append(x)
#var = ['a', 'b']

table = []


def output(table, string_trans, var, value = [0] * len(var), n = 0, count = 0):

        num = len(var)
        n = int(n % num)

        for i in range(num):
                exec(var[i] + '=value[i]')
        
        if(count == 2**(num + 1)):              #1回のループで真理値表の1マスを埋めることになるので，合計で2^(N+1)回の処理
                return None

        for i in [0, 1]:

                exec(var[n] + '=i')              #例えば，a = 1
                value[n] = i

                #最後の要素に値を代入している時，stringを実行する
                if(n == num - 1):
                        for j in range(num):
                                exec("print(eval(var[j]), end='\t')")
                        print(eval(string_trans))
                        exec('table.append(eval(var[j]))')
                else:
                        output(table, string_trans, var, value, n + 1, count + 1)


#カルノー図描画，3変数以上では転置が必要なことに注意
def karnaugh(table):
        for i in range(len(table)):
                print(table[i], end='\t')
                if((i + 1) % 2 == 0):
                        print('\n')

#隣接する真理値がどちらも1なら1を返す関数
#比較対象は基準のセルとその左(-x)と上(y)
#こうすることで境界を跨いだ1の連なりも簡単に考慮できる

# def next(table):
#         for i in range(2):
#                 for j in range(2):
#                         if(table[i, j] == table[i - 1, ])



output(table, string_trans, var)
karnaugh(table)