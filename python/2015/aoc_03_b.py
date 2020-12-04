""" Advent of Code, 2015: Day 03, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
directions = inputs[0]


def run():
    """ Use a set of tuples to remember visited house coordinates """
    houses = set({(0, 0)})
    santas = [[0, 0], [0, 0]]
    which_santa = 0
    for direction in directions:
        if direction == '^':
            santas[which_santa][1] -= 1
        if direction == 'v':
            santas[which_santa][1] += 1
        if direction == '<':
            santas[which_santa][0] -= 1
        if direction == '>':
            santas[which_santa][0] += 1
        house = (santas[which_santa][0], santas[which_santa][1])
        if (house) not in houses:
            houses.add(house)
        which_santa = 1 - which_santa
    return len(houses)


if __name__ == "__main__":
    print(run())
