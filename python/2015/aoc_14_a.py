""" Advent of Code, 2015: Day 14, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

reindeer = dict()


def race(deer):
    """ Use the reindeer's speed and rest times to find the timed distance """
    seconds, distance = 2503, 0
    stats = reindeer[deer]
    resting = False
    while True:
        if resting:
            if seconds <= stats[2]:
                break
            seconds -= stats[2]
        else:
            if seconds <= stats[1]:
                distance += seconds * stats[0]
                break
            seconds -= stats[1]
            distance += stats[1] * stats[0]
        resting = not resting
    return distance


def run():
    """ Calculate reindeer distances after a given time """
    for line in inputs:
        parts = line.split()
        reindeer[parts[0]] = [int(parts[3]), int(parts[6]), int(parts[-2])]
    furthest = 0
    for deer in reindeer:
        distance = race(deer)
        if distance > furthest:
            furthest = distance
    return furthest


if __name__ == "__main__":
    print(run())
