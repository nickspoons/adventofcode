""" Advent of Code, 2020: Day 13, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Step through increasing multipliers finding remainder patterns """
    busses = []
    for i, bus in enumerate(inputs[1].split(",")):
        if bus != "x":
            busses.append([int(bus), i])

    n = busses[0][0]
    multiplier, i_multiplier = n, 0
    while True:
        correct = True
        for bus in busses[1:]:
            if (n + bus[1]) % bus[0] != 0:
                correct = False
                break
            if bus[1] > i_multiplier:
                i_multiplier = bus[1]
                multiplier *= bus[0]

        if correct:
            return n
        n += multiplier


if __name__ == "__main__":
    print(run())
