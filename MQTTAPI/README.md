![Build succeeded][build-shield]
![Test passing][test-shield]
[![Issues][issues-shield]][issues-url]
[![Issues][closed-shield]][issues-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![License][license-shield]][license-url]

# Intelligent house
#### API (for MQTT broker)
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#summary">Summary</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#endpoints">Endpoints</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

### Summary
This is part 2 of the project (MQTT Broker with API).
The API is built upon the custom MQTT broker attached to the same database.
The API can run idependently, thus not needing the broker.

The API requires authentication to connect to it, the only endpoint that is available (for testing) is /

### Endpoints
| Endpoint | Protocol | Parameters |  Return | Description |
|-|-|-|-|-|
| /kitchen/{ts} | GET | DateTime | List of measurements | Returns *n* amount of measurements since timestamp |
| /kitchen/1 | GET | N/A | Single measurement | Returns the last known measurement |
| /bedroom/{ts} | GET | DateTime | List of measurements | Returns *n* amount of measurements since timestamp |
| /bedroom/1 | GET | N/A | Single measurement | Returns the last known measurement |
| /livingroom/{ts} | GET | DateTime | List of measurements | Returns *n* amount of measurements since timestamp |
| /livingroom/1 | GET | N/A | Single measurement | Returns the last known measurement |
| /airq/{ts} | GET | DateTime | List of measurements | Returns *n* amount of measurements since timestamp |
| /airq/1 | GET | N/A | Single measurement | Returns the last known measurement |
| /all | GET | N/A | List of logs | Returns the last 50 logs |
| /info | GET | N/A | List of logs | Returns the last 50 info-level logs |
| /debug | GET | N/A | List of logs | Returns the last 50 debug-level logs |
| /system | GET | N/A | List of logs | Returns the last 50 critical-level logs |
| /critical | GET | N/A | List of logs | Returns the last 50 system-level logs |


### Roadmap
- [X] Create API and attach it to DB
- [X] Enable authentication on API
- [X] Secure API behind firewall

### License
* GPLv3
<p align="right">(<a href="#top">back to top</a>)</p>

### Contact
- Nicolai Heuck - nicolaiheuck@gmail.com
- Jan Andreasen - jan@tved.it
  - [![Twitter][twitter-shield]][twitter-url]

Project Link: [https://github.com/Thoroughbreed/MQTT-Broker](https://github.com/Thoroughbreed/MQTT-Broker)
<p align="right">(<a href="#top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[build-shield]: https://img.shields.io/badge/Build-passed-brightgreen.svg
[test-shield]: https://img.shields.io/badge/Tests-passed-brightgreen.svg
[contributors-shield]: https://img.shields.io/github/contributors/Thoroughbreed/MQTT-Broker.svg?style=badge
[contributors-url]: https://github.com/Thoroughbreed/MQTT-Broker/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Thoroughbreed/MQTT-Broker.svg?style=badge
[forks-url]: https://github.com/Thoroughbreed/MQTT-Broker/network/members
[issues-shield]: https://img.shields.io/github/issues/Thoroughbreed/MQTT-Broker.svg?style=badge
[closed-shield]: https://img.shields.io/github/issues-closed/Thoroughbreed/MQTT-Broker?label=%20
[issues-url]: https://github.com/Thoroughbreed/MQTT-Broker/issues
[license-shield]: https://img.shields.io/github/license/Thoroughbreed/MQTT-Broker.svg?style=badge
[license-url]: https://github.com/Thoroughbreed/MQTT-Broker/blob/master/LICENSE
[twitter-shield]: https://img.shields.io/twitter/follow/andreasen_jan?style=social
[twitter-url]: https://twitter.com/andreasen_jan
