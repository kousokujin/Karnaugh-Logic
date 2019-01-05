import numpy as np



#以下を再帰に直しましょう．メリットはありますか？
count = 0
for i in range(2):
        for j in range(2):
                matrix_k[i, j] = table[count]
                count += 1


#カルノー図のサイズは変数の個数で決まってくる
num = len(var)

#appendを上手く使って次元を増やせればループで簡潔に書けるはずだが...
#一次元リストを二次元にできるのか？定義の瞬間に拡張は無理？？
if num == 1:
    matrix_k = np.empty((2, 1))
    else if num == 2:
        matrix_k = np.empty((2, 2))
    else if num == 3:
        matrix_k = np.empty((4, 2))
    else if num == 4:
        matrix_k = np.empty((4, 4))
    else if num == 5:
        matrix_k = np.empty((4, 4, 2))
    else if num == 6:
        matrix_k = np.empty((4, 4, 4))

#ifの分岐で記述すると，代入の時もifで書くことになる？


#append.var
matrix_k = np.array()
for n in range(2**len(var)):
    matrix_k.append(None)


#matrix_kがカルノー図を２次元行列に落としたもの
def karn(table, matrix_k, count = 0):
        if count == len(table):
                return

        for j in range(2):
            matrix_k[0, j] = table(count)
            count += 1
        for j in range(2):
            matrix_k[1, j - 1] = table(count)
            count += 1

#カルノー図の反転は添字を-1すれば実現できる？

#00
#01
#11
#10

#matrix_kがカルノー図を２次元行列に落としたもの
def karn(table, matrix_k, count = 0):
        if count == len(table):
                return
        
        for i in [0, 1]:
            #for j in [1, 0]
            for j in [-1, 0]
            matrix_k[i, j] = table(count)
            count += 1


#真理値表の出力を2次元行列にする
#niはカルノー図の行数，njは列数
n = len(var)
if 1 <= n <= 2:
    ni = 2
    else if 3 <= n:
        ni = 4
nj = int(2**n / ni)

table = np.reshape(table, [ni, nj])

#カルノー図への変換
#変数が4つの場合に限る
#3つなら，補完して消去するか，基本行列が変化してくる

#基本行列の生成
e = np.eye(4)
elem = np.eye(4)
elem[2, :] = e[3, :]
elem[3, :] = e[2, :]
kar = np.dot(np.dot(elem, table), elem)
