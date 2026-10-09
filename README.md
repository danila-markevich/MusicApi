# MusicApi

REST API для музыкальной библиотеки на ASP.NET Core Web API.

## Стек

- C# / .NET 10
- ASP.NET Core Web API — контроллеры, DI
- Entity Framework Core — ORM, миграции
- SQL Server — база данных

## Функциональность

- CRUD для исполнителей, песен, плейлистов
- Связь один-ко-многим (Artist → Song)
- Связь многие-ко-многим (Song ↔ Playlist)
- DTO для запросов и ответов
- Валидация входных данных (Data Annotations)
- Swagger UI для тестирования

## Эндпоинты

### Artists

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/artists | Все исполнители с количеством песен |
| GET | /api/artists/{id} | Исполнитель по Id |
| POST | /api/artists | Создать исполнителя |
| PUT | /api/artists/{id} | Обновить исполнителя |
| DELETE | /api/artists/{id} | Удалить исполнителя |

### Songs

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/songs | Все песни |
| GET | /api/songs/{id} | Песня по Id |
| POST | /api/songs | Создать песню |
| PUT | /api/songs/{id} | Обновить песню |
| DELETE | /api/songs/{id} | Удалить песню |

### Playlists

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/playlists | Все плейлисты с количеством песен |
| GET | /api/playlists/{id} | Плейлист по Id |
| POST | /api/playlists | Создать плейлист |
| PUT | /api/playlists/{id} | Обновить плейлист |
| DELETE | /api/playlists/{id} | Удалить плейлист |
| POST | /api/playlists/{playlistId}/songs/{songId} | Добавить песню в плейлист |
| DELETE | /api/playlists/{playlistId}/songs/{songId} | Убрать песню из плейлиста |
| GET | /api/playlists/{playlistId}/songs | Получить все песни плейлиста |

## Архитектура

- Models — сущности БД (Artist, Song, Playlist)
- DTOs — контракты API
- Data — DbContext
- Controllers — обработка HTTP-запросов

## Как запустить

1. Установить .NET 10 SDK и SQL Server Express.
2. Клонировать репозиторий: git clone https://github.com/danila-markevich/MusicApi.git
3. Настроить строку подключения в Program.cs.
4. Применить миграции: dotnet ef database update
5. Запустить: dotnet run
6. Открыть Swagger: https://localhost:XXXX/swagger

## Автор

Danila Markevich — https://github.com/danila-markevich