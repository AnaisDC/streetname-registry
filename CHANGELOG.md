## [1.4.2](https://github.com/informatievlaanderen/streetname-registry/compare/v1.4.1...v1.4.2) (2019-05-21)

## [1.4.1](https://github.com/informatievlaanderen/streetname-registry/compare/v1.4.0...v1.4.1) (2019-05-20)

# [1.4.0](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.12...v1.4.0) (2019-04-30)


### Features

* add projector + cleanup projection libraries ([a861da2](https://github.com/informatievlaanderen/streetname-registry/commit/a861da2))
* upgrade packages ([6d9ad96](https://github.com/informatievlaanderen/streetname-registry/commit/6d9ad96))

## [1.3.12](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.11...v1.3.12) (2019-04-18)

## [1.3.11](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.10...v1.3.11) (2019-04-17)


### Bug Fixes

* [#8](https://github.com/informatievlaanderen/streetname-registry/issues/8) + Volgende is now not emitted if null ([fe6eb46](https://github.com/informatievlaanderen/streetname-registry/commit/fe6eb46))

## [1.3.10](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.9...v1.3.10) (2019-04-16)


### Bug Fixes

* sort streetname list by olsoid [GR-717] ([f62740e](https://github.com/informatievlaanderen/streetname-registry/commit/f62740e))

## [1.3.9](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.8...v1.3.9) (2019-03-06)

## [1.3.8](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.7...v1.3.8) (2019-02-28)


### Bug Fixes

* swagger docs now show list response correctly ([79adcf9](https://github.com/informatievlaanderen/streetname-registry/commit/79adcf9))

## [1.3.7](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.6...v1.3.7) (2019-02-26)

## [1.3.6](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.5...v1.3.6) (2019-02-25)

## [1.3.5](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.4...v1.3.5) (2019-02-25)


### Bug Fixes

* extract only exports completed items ([6baf2e9](https://github.com/informatievlaanderen/streetname-registry/commit/6baf2e9))
* use new lastchangedlist migrations runner ([4d4e0e2](https://github.com/informatievlaanderen/streetname-registry/commit/4d4e0e2))

## [1.3.4](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.3...v1.3.4) (2019-02-07)


### Bug Fixes

* support nullable Rfc3339SerializableDateTimeOffset in converter ([7b3c704](https://github.com/informatievlaanderen/streetname-registry/commit/7b3c704))

## [1.3.3](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.2...v1.3.3) (2019-02-06)


### Bug Fixes

* properly serialise rfc 3339 dates ([abd5daf](https://github.com/informatievlaanderen/streetname-registry/commit/abd5daf))

## [1.3.2](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.1...v1.3.2) (2019-02-06)


### Bug Fixes

* oslo id and niscode in sync werent correctly projected ([32d9ee8](https://github.com/informatievlaanderen/streetname-registry/commit/32d9ee8))

## [1.3.1](https://github.com/informatievlaanderen/streetname-registry/compare/v1.3.0...v1.3.1) (2019-02-04)

# [1.3.0](https://github.com/informatievlaanderen/streetname-registry/compare/v1.2.3...v1.3.0) (2019-01-25)


### Bug Fixes

* correctly setting primary language in sync projection ([825ba1a](https://github.com/informatievlaanderen/streetname-registry/commit/825ba1a))
* fix starting Syndication projection ([46788bc](https://github.com/informatievlaanderen/streetname-registry/commit/46788bc))
* list now displays name of streetnames correctly ([d02b6d2](https://github.com/informatievlaanderen/streetname-registry/commit/d02b6d2))


### Features

* adapted sync with new municipality changes ([c05d427](https://github.com/informatievlaanderen/streetname-registry/commit/c05d427))
* change display municipality name of detail in Api.Legacy ([79d693f](https://github.com/informatievlaanderen/streetname-registry/commit/79d693f))

## [1.2.3](https://github.com/informatievlaanderen/streetname-registry/compare/v1.2.2...v1.2.3) (2019-01-22)


### Bug Fixes

* use https for namespace ([92965c1](https://github.com/informatievlaanderen/streetname-registry/commit/92965c1))

## [1.2.2](https://github.com/informatievlaanderen/streetname-registry/compare/v1.2.1...v1.2.2) (2019-01-18)

## [1.2.1](https://github.com/informatievlaanderen/streetname-registry/compare/v1.2.0...v1.2.1) (2019-01-18)


### Bug Fixes

* migrations history table for syndication ([f78cd51](https://github.com/informatievlaanderen/streetname-registry/commit/f78cd51))

# [1.2.0](https://github.com/informatievlaanderen/streetname-registry/compare/v1.1.2...v1.2.0) (2019-01-17)


### Features

* do not take diacritics into account when filtering on municipality ([025a122](https://github.com/informatievlaanderen/streetname-registry/commit/025a122))

## [1.1.2](https://github.com/informatievlaanderen/streetname-registry/compare/v1.1.1...v1.1.2) (2019-01-16)


### Bug Fixes

* required upgrade for datadog tracing to avoid connection pool problems ([432dbb4](https://github.com/informatievlaanderen/streetname-registry/commit/432dbb4))

## [1.1.1](https://github.com/informatievlaanderen/streetname-registry/compare/v1.1.0...v1.1.1) (2019-01-16)


### Bug Fixes

* optimise catchup mode for versions ([4583327](https://github.com/informatievlaanderen/streetname-registry/commit/4583327))

# [1.1.0](https://github.com/informatievlaanderen/streetname-registry/compare/v1.0.1...v1.1.0) (2019-01-16)


### Bug Fixes

* legacy syndication now subsribes to OsloIdAssigned ([42f0f49](https://github.com/informatievlaanderen/streetname-registry/commit/42f0f49))
* take local changes into account for versions projection ([9560ec6](https://github.com/informatievlaanderen/streetname-registry/commit/9560ec6))


### Features

* add statuscode 410 Gone for removed streetnames ([4e5f7f6](https://github.com/informatievlaanderen/streetname-registry/commit/4e5f7f6))

## [1.0.1](https://github.com/informatievlaanderen/streetname-registry/compare/v1.0.0...v1.0.1) (2019-01-15)


### Bug Fixes

* streetnameid in extract file is a string ([f845424](https://github.com/informatievlaanderen/streetname-registry/commit/f845424))

# 1.0.0 (2019-01-14)


### Features

* open source with EUPL-1.2 license as 'agentschap Informatie Vlaanderen' ([bba50fd](https://github.com/informatievlaanderen/streetname-registry/commit/bba50fd))
