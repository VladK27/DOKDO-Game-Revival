# build_status.md

Восстановление DOKDO v1.16.6 → минимальный игровой ARM64-билд (Pixel 9, Android 17).
Реклама/покупки/Facebook/аналитика/GPG на этом этапе исключены.

## Окружение
- Источник проекта: `work/assetripper_export/ExportedProject` (Unity 5.6.5f1, Mono)
- Рабочая копия: `work/dokdo_unity2019`
- Целевой движок: **Unity 2019.4 LTS**, Android Build Support (SDK+NDK), IL2CPP, ARM64

## Статус шагов
| Шаг | Статус | Примечание |
|-----|--------|-----------|
| 1.1 Рабочая копия | ✅ | `work/dokdo_unity2019` |
| 1.2 Git baseline | ✅ | коммит baseline |
| 1.3 Проверка Unity 2019.4 | ❌ | **Unity НЕ установлен** (нет Hub/Editor/NDK/IL2CPP) |
| 1.4 Отчёт | ✅ | данный файл |
| 2. Компиляция в 2019.4 | ⛔ blocked | ждёт установки Unity |
| 3. Изоляция SDK | ⛔ blocked | |
| 4. Первый запуск ARM64 | ⛔ blocked | |

## Блокер
Unity 2019.4 LTS отсутствует. Автоустановка/смена версии не выполняется (по указанию).
