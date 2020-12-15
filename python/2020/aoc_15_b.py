""" Advent of Code, 2020: Day 15, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Maintain a dictionary of last-positions instead of counting """
    numbers = [int(n) for n in inputs[0].split(",")]
    seen = {n: i for (i, n) in enumerate(numbers)}
    last = -1
    index, target = len(seen) - 1, 30000000
    while True:
        age = 0
        if last in seen:
            age = index - seen[last]
        seen[last] = index
        if index + 2 == target:
            return age
        index += 1
        last = age
    return 0


if __name__ == "__main__":
    print(run())
