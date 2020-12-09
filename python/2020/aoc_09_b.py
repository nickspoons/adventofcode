""" Advent of Code, 2020: Day 09, b """

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
    """ Keep a running range of contigious values to find the ones that sum to
    the first invalid element """
    for i in range(P_SIZE, len(inputs) - 1):
        if not valid(i):
            target = inputs[i]
            target_index = i
            break

    weakness = []
    running = 0
    for i in range(target_index):
        weakness.append(inputs[i])
        running += inputs[i]
        while running > target:
            running -= weakness[0]
            weakness = weakness[1:]
        if running == target:
            return min(weakness) + max(weakness)

    return 0


if __name__ == "__main__":
    print(run())
