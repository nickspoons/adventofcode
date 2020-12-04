""" Advent of Code, 2020: Day 04, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def run():
    """ Validate passports by counting number of fields """
    passports = []
    passport = []
    for line in inputs:
        if line.strip() == "":
            passports.append(passport)
            passport = []
        else:
            passport += line.strip().split(' ')
    passports.append(passport)

    valid = 0
    for passport in passports:
        fields = {f.split(':')[0] for f in passport}
        if 'cid' in fields:
            fields.remove('cid')
        if len(fields) == 7:
            valid += 1

    return valid


if __name__ == "__main__":
    print(run())
