""" Advent of Code, 2020: Day 16, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

all_fields = set()  # For excluding invalid tickets
fields = dict()  # For matching ticket field types
tickets = []


def parse_input():
    """ Read rules and tickets """
    mode = 0
    for line in inputs:
        if "ticket" in line:
            continue
        if not line:
            mode += 1
        elif mode == 0:
            # Read ticket rules
            section = line.split(":")[0]
            fields[section] = set()
            for rng in line.split(": ")[1].split(" or "):
                a, b = [int(p) for p in rng.split("-")]
                fields[section].update(list(range(a, b + 1)))
            all_fields.update(fields[section])
        elif mode > 0:
            # Read nearby tickets
            tickets.append([int(n) for n in line.split(",")])


def remove_invalid():
    """ Remove invalid tickets """
    i = 0
    while i < len(tickets):
        if all([f in all_fields for f in tickets[i]]):
            i += 1
        else:
            tickets.remove(tickets[i])


def match_fields():
    """ Match ticket fields to field sections """
    n = len(tickets[0])
    positions = []
    for i in range(n):
        positions.append(list(fields.keys()))
    # Check each field position against all tickets and remove fields which do
    # not match for every ticket
    for i in range(n):
        for ticket in tickets:
            for field in positions[i]:
                if ticket[i] not in fields[field]:
                    positions[i].remove(field)
                    break
    # Any field which is only possible in a single position can be removed from
    # the others, until there are only single possibilities remaining
    cleared = set()
    while max([len(positions[i]) for i in range(n)]) > 1:
        for i in range(n):
            field = positions[i][0]
            if len(positions[i]) == 1 and field not in cleared:
                for j in range(n):
                    if i != j and field in positions[j]:
                        positions[j].remove(field)
                cleared.add(field)
    return [position[0] for position in positions]


def multiply_departures(field_positions):
    """ Find the product of the departure fields from my ticket """
    departure_values = 1
    for i, field in enumerate(field_positions):
        if field[:9] == "departure":
            departure_values *= tickets[0][i]
    return departure_values


def run():
    """ Use a dict of field types to match ticket fields to field types """
    parse_input()
    remove_invalid()
    field_positions = match_fields()
    return multiply_departures(field_positions)


if __name__ == "__main__":
    print(run())
