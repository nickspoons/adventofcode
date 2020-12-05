""" Advent of Code, 2020: Day 05, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Calculate all ids and sort them, then find a gap of 2 """
    ids = []
    for seat in inputs:
        row = int(seat[:7].replace("F", "0").replace("B", "1"), 2)
        column = int(seat[7:].replace("L", "0").replace("R", "1"), 2)
        ids.append(row * 8 + column)
    ids = sorted(ids)
    for i in range(len(ids) - 1):
        if ids[i + 1] == ids[i] + 2:
            return ids[i] + 1

    return 0


if __name__ == "__main__":
    print(run())
