""" Advent of Code, 2020: Day 09, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [int(line.strip()) for line in f]

P_SIZE = 25


def valid(index):
    """ Check if the indexed element is the sum of 2 of the previous 25 """
    for i in range(index - P_SIZE, index - 1):
        for j in range(i + 1, index):
            if inputs[i] + inputs[j] == inputs[index]:
                return True
    return False


def run():
    """ Find the first invalid number, after the 25 element preamble """
    for i in range(P_SIZE, len(inputs) - 1):
        if not valid(i):
            return inputs[i]
    return 0


if __name__ == "__main__":
    print(run())
