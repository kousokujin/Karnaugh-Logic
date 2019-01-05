import numpy as np

def product(i, j, nx, ny, matrix, hoge = 1):

    for coord_x in range(nx):
        for coord_y in range(ny):
            hoge *= matrix[coord_x, coord_y]
    
    return int(hoge)

matrix_x = np.ones((2, 2))
print(product(1, 1, 1, 0, matrix_x))
