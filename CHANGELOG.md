# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- More functionality for demo scene
    - Visualization of all Audio Mixer groups
    - Amount of active clips per group
    - All active audio sources and their parameters
    - Acknowledgements and credits
    - Method overloads for `PlayOnce()`, `PlayOnLoop()`, `PlayAtPosition()` and `FadeGroupVolume()`
- Demo scene installation instructions to README

### Fixed
- Fixed setting group volumes to negative values
- Fixed fading group volumes to negative values
- Fixed invalid exposed parameter in `FadeGroupVolumeTo()` method
- Fixed Audio Mixer visualization in Demo Scene

### Changed
- Clarify setup instructions in README
- Remame Sample Scene to DemoScene

## [1.0.0] - 2023-12-30

- Initial release