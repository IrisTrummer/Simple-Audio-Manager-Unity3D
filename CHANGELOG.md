# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.0] - 2024-07-22

### Changed

- Changed `SoundEffect` struct to class
- Changed default value of sound effect volume to `1f`

## [1.0.2] - 2024-03-09

### Added

- Added sound effect struct (helper containing an audio clip and a volume) with custom property drawer
- Added "Play"-method extensions accepting a sound effect helper

### Changed

- Audio Source time property is now automatically reset before it is played
- Inactive audio sources no longer get disabled

### Fixed

- Fixed a bug that caused very short clips to sometimes not be audible

## [1.0.1] - 2024-01-01

### Added

- More functionality for demo scene
- Demo scene installation instructions to README
- Screenshots for Setup instructions to README

### Changed

- Clarify setup instructions in README
- Remame Sample Scene to DemoScene

### Fixed

- Fixed setting group volumes to negative values
- Fixed fading group volumes to negative values
- Fixed invalid exposed parameter in `FadeGroupVolumeTo()` method
- Fixed Audio Mixer visualization in Demo Scene

## [1.0.0] - 2023-12-30

### Added

- Initial release
