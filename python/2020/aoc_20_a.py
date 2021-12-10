""" Advent of Code, 2020: Day 20, a """

from math import prod

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def parse_input():
    """ Read the input into distinct tiles (lists of edges) """
    tiles = []
    tile = [""] * 4     # list of 4 strings, in CSS order
    id = 0
    for line in inputs:
        if line == "":
            if tile[0] != "":
                tiles.append({"id": id, "tile": tile})
            tile = [""] * 4     # list of 4 strings, in CSS order
        elif line[:4] == "Tile":
            id = int(line[5:-1])
        else:
            if tile[0] == "":
                tile[0] = line  # top
            tile[3] += line[0]  # left
            tile[1] += line[-1] # right
            tile[2] = line      # bottom
    return tiles


def find_variants(tile):
    """ Find all flipped/rotated variants of a tile """
    t = tile[0]
    r = tile[1]
    b = tile[2]
    l = tile[3]
    tr = "".join(reversed(tile[0]))
    rr = "".join(reversed(tile[1]))
    br = "".join(reversed(tile[2]))
    lr = "".join(reversed(tile[3]))
    return [
        [t,   r,  b,   l ], # orig
        [b,   rr, t,   lr], # flip-x
        [tr,  l,  br,  r ], # flip-y
        [br,  lr, tr,  rr], # flip-xy, or rotate-180
        [r,   br, l,   tr], # rotate-left
        [l,   b,  r,   t],  # rotate-left, then flip-x
        [rr,  tr, lr,  br], # rotate-left, then flip-y
        [lr,  t,  rr,  b]   # rotate-right
    ]


def match_pair(tilea, tileb):
    """ Check whether two tiles have match edges """
    match_edges = [[0, 2], [2, 0], [1, 3], [3, 1]]
    for variant in find_variants(tileb["tile"]):
        if any([tilea["tile"][m[0]] == variant[m[1]] for m in match_edges]):
            return True
    return False


def find_corners(tiles):
    """ Find the tiles which only match on 2 edges """
    corners = []
    for n, tilea in enumerate(tiles):
        matches = 0
        for m, tileb in enumerate(tiles):
            if (n == m):
                continue
            if match_pair(tilea, tileb):
                matches += 1
                if matches > 2:
                    continue
        if matches == 2:
            corners.append(tilea)
    return corners


def run():
    """ Arrange/flip/rotate tiles to make a board """
    tiles = parse_input()
    corners = find_corners(tiles)
    return prod([tile["id"] for tile in corners])


if __name__ == "__main__":
    print(run())
