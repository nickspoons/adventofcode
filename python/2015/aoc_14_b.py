""" Advent of Code, 2015: Day 14, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

reindeer = dict()


def race(deer, seconds):
    """ Use the reindeer's speed and rest times to find the timed distance """
    distance = 0
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
    points = dict()
    for deer in reindeer:
        points[deer] = 0
    for seconds in range(1, 2504):
        distances = dict()
        for deer in reindeer:
            distances[deer] = race(deer, seconds)
        furthest = max(distances.values())
        for deer in reindeer:
            if distances[deer] == furthest:
                points[deer] += 1
    return max(points.values())


if __name__ == "__main__":
    print(run())
