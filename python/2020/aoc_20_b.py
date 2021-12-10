""" Advent of Code, 2020: Day 20, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def parse_input():
    """ Read the input into distinct tiles (lists of strings) """
    tiles = []
    for line in inputs:
        if line == "":
            tiles.append(tile)
        elif line[:4] == "Tile":
            tile = []
        else:
            tile.append(list(line))
    tiles.append(tile)
    return tiles


def flipx(tile):
    """ Return a copy of the tile, flipped horizontally """
    return list(reversed(tile))


def flipy(tile):
    """ Return a copy of the tile, flipped vertically """
    return list(list(reversed(l)) for l in tile)


def rotate(tile):
    """ Return a copy of the tile, rotated right """
    return list(zip(*reversed(tile)))


def find_variants(tile):
    """ Find all flipped/rotated variants of a tile """
    variants = [tile]
    for i in range(3):
        variants.append(rotate(variants[-1]))
    variants.append(flipx(tile))
    variants.append(flipy(tile))
    variants.append(flipx(variants[1]))
    variants.append(flipy(variants[1]))
    return variants


def match_below(a, b):
    return all(a[-1][x] == b[0][x] for x in range(len(a)))


def match_right(a, b):
    return all(a[y][-1] == b[y][0] for y in range(len(a)))


def find_first(tiles):
    """ Find the top-left tile for the board """
    for n, tile in enumerate(tiles):
        rotations = [tile]
        for i in range(3):
            rotations.append(rotate(rotations[-1]))
        for rot in rotations:
            matched = False
            for m, tmatch in enumerate(tiles):
                if n == m:
                    continue
                for v in find_variants(tmatch):
                    if match_right(v, rot) or match_below(v, rot):
                        matched = True
                        break
                if matched:
                    break
            if not matched:
                tiles.remove(tile)
                return rot


def find(tiles, board, x, y):
    """ Find the correct tile for the board co-ordinate """
    for tile in tiles:
        for variant in find_variants(tile):
            if x != 0:
                matched = match_right(board[y][x-1], variant)
            else:
                matched = match_below(board[y-1][x], variant)
            if matched:
                tiles.remove(tile)
                return variant


def build_board(tiles):
    """ Match up edges of tiles """
    size = int(len(tiles) ** 0.5)
    board = [[None for i in range(size)] for j in range(size)]
    board[0][0] = find_first(tiles)
    for y in range(0, size):
        for x in range(0, size):
            if x > 0 or y > 0:
                board[y][x] = find(tiles, board, x, y)
    # Remove borders and join up the board
    result = []
    for by in board:
        for l in range(1, len(by[0]) - 1):
            row = []
            for b in by:
                row += b[l][1:-1]
            result.append(row)
    return result


def match_monster(board, pattern, x, y):
    """ Check if the given coordinates match the monster pattern """
    for py, row in enumerate(pattern):
        if py + y >= len(board):
            return False
        for px, c in enumerate(row):
            if px + x >= len(board[0]):
                return False
            if c == "#":
                if board[y + py][x + px] != "#":
                    return False
    return True


def paint_monsters(board, pattern):
    """ Find all the monsters and change their # to 0 """
    h = len(pattern)
    w = len(pattern[0])
    for y in range(len(board) + 1 - h):
        for x in range(len(board[0]) + 1 - w):
            if match_monster(board, pattern, x, y):
                for py, row in enumerate(pattern):
                    for px, c in enumerate(row):
                        if c == "#":
                            board[y + py][x + px] = "0"


def find_monsters(board):
    """ Find the board rotation containing monster patterns and fill them in """
    pattern = [
        list("                  # "),
        list("#    ##    ##    ###"),
        list(" #  #  #  #  #  #   ")
    ]
    h = len(pattern)
    w = len(pattern[0])
    for v, variant in enumerate(find_variants(board)):
        for y, row in enumerate(variant):
            for x, c in enumerate(row):
                if match_monster(variant, pattern, x, y):
                    paint_monsters(variant, pattern)
                    return variant


def run():
    """ Arrange/flip/rotate tiles to make a board """
    tiles = parse_input()
    board = build_board(tiles)
    board = find_monsters(board)
    return sum(sum(1 if c == "#" else 0 for c in line) for line in board)


if __name__ == "__main__":
    print(run())
