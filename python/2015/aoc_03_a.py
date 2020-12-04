""" Advent of Code, 2015: Day 03, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
directions = inputs[0]


def run():
    """ Use a set of tuples to remember visited house coordinates """
    houses = set({(0, 0)})
    x, y = 0, 0
    for direction in directions:
        if direction == '^':
            y -= 1
        if direction == 'v':
            y += 1
        if direction == '<':
            x -= 1
        if direction == '>':
            x += 1
        if (x, y) not in houses:
            houses.add((x, y))
    return len(houses)


if __name__ == "__main__":
    print(run())
