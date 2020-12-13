""" Advent of Code, 2020: Day 13, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Find the first time each bus passes the busstop """
    timestamp = int(inputs[0])
    busses = [int(b) for b in inputs[1].split(",") if b != "x"]
    min_w = 99999
    for bus in busses:
        wait = (timestamp // bus + 1) * bus - timestamp
        if wait < min_w:
            min_w = wait
            answer = wait * bus

    return answer


if __name__ == "__main__":
    print(run())
