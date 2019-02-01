import json
import numpy as np
import simplification

karn = simplification.karn
var = simplification.var
dim = len(var)
nx = simplification.nx
ny = simplification.ny
#dict型はnumpy emptyには格納できない．空の2次元配列を作るリスト内包表記
val_map = [[None] * nx for j in range(ny)]


#val_mapは２次元の辞書型で用意したほうがわかりやすい，np.reshape(val_map, (1, nx*ny))

for i in range(nx):
    for j in range(ny):
        val_map[i][j] = {
            'x': i,
            'y': j,
            'z': 0,
            'value': bool(karn[i, j]),
            'block_id': [0]
            }

def add_block_id(val_map, circleinst):
    max_id = 0
    for i in range(nx):
        for j in range(ny):
            if max_id < max(val_map[i][j]['block_id']):
                max_id = max(val_map[i][j]['block_id'])
    id_num = max_id
    sizex = circleinst.sizex
    sizey = circleinst.sizey
    for i in range(nx):
        for j in range(ny):
            if circleinst.circle[i, j]:
                id_num += 1
                for x in range(sizex):
                    for y in range(sizey):
                        val_map[i - x][j - y]['block_id'].append(id_num)

add_block_id(val_map, simplification.size12)
add_block_id(val_map, simplification.size21)
add_block_id(val_map, simplification.size22)
add_block_id(val_map, simplification.size14)
add_block_id(val_map, simplification.size41)
add_block_id(val_map, simplification.size24)
add_block_id(val_map, simplification.size42)

#JSON出力のため，１次元に直す
val_map = np.reshape(val_map, nx*ny)
val_map = val_map.tolist()

py_value = {
    "values": var,
    "dim": len(var)
}

#辞書Aと辞書Bを結合する
py_value['map'] = val_map
print(json.dumps(py_value, indent=4))

# with open('py_circle.json', 'w') as f:
#     json.dump(py_value, f, indent=4)

'''
SAMPLE
{
    "values":["value1","value2","value3"],
    "dim": 2,
    
    "map": [
        {
            "x":0,
            "y":0,
            "z":0,
            "value":true,
            "block_id":1
        },
        {
            "x":1,
            "y":0,
            "z":0,
            "value":true,
            "block_id":1
        },
        {
            "x":0,
            "y":1,
            "z":0,
            "value":true,
            "block_id":1
        },
        {
            "x":1,
            "y":1,
            "z":0,
            "value":true,
            "block_id":1
        }
    ]

}
'''