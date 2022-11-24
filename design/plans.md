# Notes

## Places in the town of Cheeseshire
- The Manor / Cheesery
- Town Square
    - Inn (Milk & Honey Tavern and Inn)
    - Tailor Shop (Huges' Haberdashery)
    - Blacksmith
    - Church (Church of the Herald)
        - Cemetery
- Farm (Mossy Oak Farm)
- Woods
    - Sewers (entrance)

## People
- Lord Apodemus (BIG CHEESE, is a weremouse)
    - Oliver Wells - Butler
- Meredith Farnsby - Inn keeper
- Liam Farnsby - Inn keeper's husband (sewer scavenger)
- Father Henry Brooks - Priest (is a prick, of couse, also a werecat)
    - Rudy Duncan - Gravedigger (doesn't like new priest who steals money from flower fund)
- Charles Huges - Tailor (has the hots for Oliver)
- Samuel Matthews - Farmer
- Edmund Fitch - Blacksmith

## Puzzle Dependency Charts
### Overview
```mermaid
flowchart
    a[Figure out how to break into the Manor] --> b[Get to the Cheese]
    b --> c[Defeat the werecat]
```

### Breaking into the Manor
```mermaid
flowchart
    a[GAME INTRO]-->b
    a-->c
    a-->d
    b[man:talk to inn keeper, learn that she needs eggs]-->n
    c[man:talk to blacksmith, learn he lost his key]-->m
    d[man:talk to tailor, learn about his love]-->u
    m[mouse:find blacksmith's lost key in manor garden]-->s
    n[mouse:steal eggs from farmer]-->o
    o[man:get tab from inn keeper]-->r
    r[man:get inn keeper's husband drunk]-->x
    s[man:get horseshoes from blacksmith]-->t
    t[man:get clippers from farmer] --> v
    u[man:deliver note to butler] --> v
    v[man:get flowers from tailor's garden] --> w
    w[man:get pick axe from gravedigger] --> y
    x[man:learn about sewer from inn keeper's husband] --> p
    p[man:find note near sewer entrance]-->q
    q[mouse:steal sewer key from priest]-->y
    y[man:drain sewer using key and pick axe] --> z[mouse:go through sewers to manor]
```

### Get to the Cheese
```mermaid
flowchart
    a[mouse:enter kitchen through sewers]
    a-->b
    a-->c
    a-->d
    b[mouse:knock over vase to distract maid]-->z
    c[mouse:ring bell to distract butler]-->z
    d[mouse:start a fire to distract cook]-->z
    z[mouse:sneak into vault]
```

### Werecat
- Maze with mousetraps??