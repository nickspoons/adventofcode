""" Advent of Code, 2015: Day 01, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
floors = inputs[0]


def run():
    """ Iterate through the directions until reaching floor -1 """
    floor = 0
    for position, direction in enumerate(floors):
        floor += 1 if direction == '(' else -1
        if floor == -1:
            return position + 1
    return 0


if __name__ == "__main__":
    print(run())
