# カルノー図JSON

カルノー図のJSON形式

'''json
 {
    "values":[変数名(string配列)],
    "dim": 次元数(2 or 3 int),
    
    "map": [
        {
            "x":x座標(int),
            "y":y座標(int),
            "z":z座標(int),
            "value":値("true","false","null"),
            "block_id":ブロック化id(int)
        }
    ]

}

'''
