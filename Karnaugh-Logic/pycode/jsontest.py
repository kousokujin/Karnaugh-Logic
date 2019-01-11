import os

name = os.path.dirname(os.path.abspath(__file__))
path = '../../TestJson.json'
joinpath = os.path.join(name,path)
datapath = os.path.normpath(joinpath)

with open(datapath) as f:
    s = f.read()
    print(s)
