""" Advent of Code, 2020: Day 16, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Read all field ranges into a set for ticket field comparison """
    fields = set()
    nearbys = []
    mode = 0
    for line in inputs:
        if "ticket" in line:
            continue
        if not line:
            mode += 1
        elif mode == 0:
            # Read ticket rules
            for rng in line.split(": ")[1].split(" or "):
                a, b = [int(p) for p in rng.split("-")]
                fields.update(list(range(a, b + 1)))
        elif mode == 1:
            # Ignore my ticket for now
            mode += 1
        else:
            # Read nearby tickets
            nearbys.extend([int(n) for n in line.split(",")])
    rate = 0
    for field in nearbys:
        if field not in fields:
            rate += field
    return rate


if __name__ == "__main__":
    print(run())
