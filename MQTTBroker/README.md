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
    <li><a href="#topics">Topics</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

### Summary
This is part 1 of the project (MQTT Broker with API).
The broker is custom made, and saves every incoming message to the database
The broker can run idependently, thus not needing the API (or DB).

The broker requires authentication to connect to it.

### Topics
| Topic | Description |
|-|-|
| home/alarm/alarm                     | Goes high when an alarm is triggered
| home/alarm/arm                       | 0-1-2 (Disarmed, Partial armed, Fully armed)
| home/climate/bedroom/sethumid        | Sets the humidity SP in bedroom
| home/climate/bedroom/settemp         | Sets the temperature SP in bedroom
| home/climate/kitchen/sethumid        | Sets the humidity SP in kitchen
| home/climate/kitchen/settemp         | Sets the temperature SP in kitchen
| home/climate/livingroom/sethumid     | Sets the humidity SP in livingroom
| home/climate/livingroom/settemp      | Sets the temperature SP in livingroom
| home/climate/status/airquality       | Air quality status
| home/climate/status/bedroom/humid    | Bedroom humidity status
| home/climate/status/bedroom/temp     | Bedroom temperature status
| home/climate/status/kitchen/humid    | Kitchen humidity status
| home/climate/status/kitchen/temp     | Kitchen temperature status
| home/climate/status/livingroom/humid | Livingroom humidity status
| home/climate/status/livingroom/temp  | Livingroom temperature status
| home/log/critical/alarm              | Log for alarm (critical level)
| home/log/critical/climate            | Log for climate (critical level)
| home/log/critical/home               | Log for home (critical level)
| home/log/debug/climate               | Log for climate (debug level)
| home/log/info/alarm                  | Log for alarm (informative level)
| home/log/info/climate                | Log for climate (informative level)
| home/log/info/home                   | Log for home (informative level)
| home/log/system                      | System log
| home/log/user                        | User log


### Roadmap
- [X] Create custom broker
- [X] Add authentication
- [X] Enable write to DB
- [X] Secure broker behind firewall
- [ ] Enable SSL connection

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
