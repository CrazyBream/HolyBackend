/* ==========================================================================
   HOLY BACKEND — THEME ENGINE & DEEP DOM TRANSLATOR (UA / EN)
   ========================================================================== */

const i18nDict = {
    uk: {
        "nav.home": "Головна",
        "nav.about": "Про Орден",
        "nav.services": "Послуги",
        "nav.portfolio": "Портфоліо",
        "nav.stats": "Статистика",
        "nav.sponsors": "Спонсори",
        "nav.grimoire": "Гримуар",
        "nav.orderBtn": "+ Замовити обряд",
        "nav.loginBtn": "Увійти в Храм",
        "menu.profile": "Редагувати профіль",
        "menu.newOrder": "Замовити новий обряд",
        "menu.grimoire": "Гримуар Клієнтів (CRM)",
        "menu.logout": "Вийти з Храму",
        "footer.motto": "Нехай ваш код буде чистим, а деплой — успішним.",
        "hero.desc": "Ми, <strong>Орден Багоугодних</strong>, створюємо сайти та складні веб-додатки на замовлення, використовуючи міць технологій <span class=\"tech-highlight\">.NET</span>, <strong>Clean Architecture</strong> та <strong>PostgreSQL</strong>.",
        "hero.quote": "«Наш код чистий, як сповідь,<br />а архітектура надійна, як фундамент собору.»",
        "hero.cta": "<span>Замовити екзорцизм коду</span><span style=\"color: var(--accent-blue);\">✦</span>",
        "hero.eyeHint": "Натисніть на Око, щоб здійснити імпульс очищення"
    },
    en: {
        "nav.home": "Home",
        "nav.about": "The Order",
        "nav.services": "Services",
        "nav.portfolio": "Portfolio",
        "nav.stats": "Statistics",
        "nav.sponsors": "Sponsors",
        "nav.grimoire": "Grimoire",
        "nav.orderBtn": "+ Request Ritual",
        "nav.loginBtn": "Enter Sanctuary",
        "menu.profile": "Edit Profile",
        "menu.newOrder": "Initiate New Ritual",
        "menu.grimoire": "Client Grimoire (CRM)",
        "menu.logout": "Leave Sanctuary",
        "footer.motto": "May your code be pure, and your deployment blessed.",
        "hero.desc": "We, the <strong>Order of the Bug-Righteous</strong>, forge bespoke websites and enterprise web applications powered by <span class=\"tech-highlight\">.NET</span>, <strong>Clean Architecture</strong>, and <strong>PostgreSQL</strong>.",
        "hero.quote": "“Our code is as pure as confession,<br />and our architecture as steadfast as a cathedral foundation.”",
        "hero.cta": "<span>Order Code Exorcism</span><span style=\"color: var(--accent-blue);\">✦</span>",
        "hero.eyeHint": "Click the Omnissiah Eye to emit a purification pulse"
    }
};

const fullTextMap = {
    // --- НАВІГАЦІЯ, ІМЕНА ТА СПІЛЬНІ ЕЛЕМЕНТИ ---
    "Верховний Інквізитор": "High Inquisitor",
    "✦ Верховний Інквізитор": "✦ High Inquisitor",
    "✦ Інквізитор": "✦ Inquisitor",
    "✦ Парафіянин": "✦ Parishioner",
    "✦ Новачок Ордену": "✦ Order Neophyte",
    "✦ Постійний парафіянин": "✦ Devout Parishioner",
    "✦ Магістр Ордену • Святий Спонсор": "✦ Grandmaster • Holy Sponsor",
    "✦ Почесний Спонсор Ордену": "✦ Honorary Order Sponsor",
    "Член Ордену": "Member of the Order",
    "Архів Сповідей (Кабінет)": "Confession Archive (Cabinet)",
    "Мої замовлення (Кабінет)": "My Orders (Cabinet)",
    "БЛАГА ЗВІСТКА ОРДЕНУ": "BLESSED TIDINGS OF THE ORDER",
    "ПОПЕРЕДЖЕННЯ ІНКВІЗИЦІЇ": "INQUISITION WARNING",
    "Натисніть, щоб відкрити Кімнату Сповіді →": "Click to open the Confession Room →",
    "Євгеній Дроздовський": "Yevhenii Drozdovskyi",
    "Анастасія Гольц": "Anastasiia Holts",
    "Брат Андрій": "Brother Andrii",
    "Брат Архітектор": "Brother Architect",

    // --- ГОЛОВНА (INDEX) ---
    "Ритуали Ордену →": "Order Rituals →",
    "⚔️ Відкрити Архів Сповідей": "⚔️ Open Confession Archive",
    "Безгрішний Uptime": "Sinless Uptime",
    "Чиста Архітектура": "Clean Architecture",
    "PostgreSQL Храм": "PostgreSQL Sanctuary",

    // --- ПРО ОРДЕН (ABOUT) ---
    "Про Орден": "About the Order",
    "Наша місія — створювати краще": "Our mission is to forge digital perfection",
    "Орден Багоугодних": "Order of the Bug-Righteous",
    "— це спільнота жреців коду, які вірять у безгрішну логіку, естетичний дизайн та непорушну архітектуру.": "— is a brotherhood of code priests who believe in sinless logic, aesthetic design, and unbreakable architecture.",
    "Ми об'єднуємо інженерну дисципліну, візуальне мистецтво та міць технологій": "We unite engineering discipline, visual artistry, and the power of",
    "і": "and",
    ", щоб перетворювати складні бізнес-ідеї на довершені цифрові собори.": "to transform complex business ideas into perfected digital cathedrals.",
    "I. Чистий Код": "I. Pure Code",
    "II. Візуальний Канон": "II. Visual Canon",
    "III. Твердиня Даних": "III. Data Fortress",
    "Канон Перший: Чистота Архітектури": "First Canon: Purity of Architecture",
    "Кожен проєкт будується за законами Clean Architecture та патернів DDD. Ми ізолюємо бізнес-ядро від зовнішнього хаосу, щоб система залишалася гнучкою та вічною.": "Every project is built upon Clean Architecture and DDD patterns. We isolate the business core from external chaos so the system remains flexible and eternal.",
    "Канон Другий: Естетична Інквізиція": "Second Canon: Aesthetic Inquisition",
    "Інтерфейс має викликати благоговіння. Кожен піксель, шрифт та мікровзаємодія проходять суворий візуальний відбір перед тим, як постати перед очима користувача.": "An interface must inspire awe. Every pixel, font, and micro-interaction undergoes strict visual inquisition before appearing before the user.",
    "Канон Третій: Транзакційна Непорушність": "Third Canon: Transactional Integrity",
    "Жодна сповідь клієнта не загубиться. Ми використовуємо атомарні транзакції Unit of Work та захищені хмарні кластери PostgreSQL (Supabase).": "Not a single client confession shall be lost. We employ atomic Unit of Work transactions and secured PostgreSQL (Supabase) cloud clusters.",
    "Переглянути наші діяння →": "Behold Our Deeds →",
    "Пантеон Спонсорів": "Pantheon of Sponsors",
    "Верховний Жрець Коду": "High Priest of Code",
    "Інквізиторка Дизайну": "Inquisitor of Design",
    "Служіння": "Ministry",
    "Арсенал і Ролі": "Arsenal & Roles",
    "Перетворює ідеї у конкретні завдання та об'єднує проєкт в єдине ціле. Відповідає за логіку розробки, налаштування серверів та проєктування баз даних. Здатен вигнати будь-якого демона (": "Transforms ideas into concrete tasks and unites the project into a single whole. Responsible for backend logic, server configuration, and database design. Capable of exorcising any daemon (",
    ") із системи.": ") from the system.",
    "Джерело креативності команди та сполучна ланка із зовнішнім світом. Оцінює ідеї з точки зору візуалу, педантично доводить їх до ідеалу та перетворює темряву порожнього екрана на естетично досконалі шедеври.": "The creative wellspring of the team and its link to the outside world. Evaluates ideas through a visual lens, meticulously refines them to perfection, and turns the darkness of a blank screen into aesthetic masterpieces.",
    "UI/UX Візуал та Композиція": "UI/UX Visuals & Composition",
    "Дизайн-системи та Естетика": "Design Systems & Aesthetics",
    "Голова": "Chairman",
    "Організатор": "Organizer",
    "Формувальник": "Shaper",
    "Генератор ідей": "Idea Generator",
    "Завершувач": "Completer-Finisher",
    "Оцінювач ідей": "Evaluator",
    "Дослідник": "Resource Investigator",
    "Організатор групи": "Teamworker",
    "Архітектура • Логіка • Стабільність": "Architecture • Logic • Stability",
    "Краса • Гармонія • Візуал": "Beauty • Harmony • Visuals",

    // --- ПОСЛУГИ (SERVICES) ---
    "Послуги Ордену": "Order Services",
    "Ритуали цифрового спасіння будь-якої складності": "Rituals of digital salvation of any complexity",
    "Оберіть потрібне таїнство зі списку нижче, щоб оглянути архітектурну печатку, складові обряду та канони його виконання.": "Select a sacrament from the list below to inspect its architectural seal, deliverables, and execution canons.",
    "Зведення Веб-Собору під ключ": "Turnkey Web Cathedral Construction",
    "Комплексна розробка веб-сайтів та корпоративних систем на .NET 8 MVC з нуля до деплою.": "End-to-end development of websites and enterprise systems on .NET 8 MVC from ground up to deployment.",
    "Архітектура Ядра та Сховищ Даних": "Core Architecture & Data Vaults",
    "Проєктування Clean Architecture, REST API, реляційних схем PostgreSQL (Supabase) та патернів DDD.": "Designing Clean Architecture, REST APIs, relational PostgreSQL (Supabase) schemas, and DDD patterns.",
    "Естетична Інквізиція та UI/UX Дизайн": "Aesthetic Inquisition & UI/UX Design",
    "Авторські дизайн-системи, інтерактивна SVG-графіка, 3D-мікровзаємодії та адаптивна верстка.": "Bespoke design systems, interactive SVG graphics, 3D micro-interactions, and responsive layouts.",
    "Екзорцизм Легасі-Коду та Рефакторинг": "Legacy Code Exorcism & Refactoring",
    "Вигнання критичних багів, оптимізація повільних SQL-запитів, лікування Deadlock-ів та прискорення систем.": "Exorcising critical bugs, optimizing slow SQL queries, healing deadlocks, and accelerating systems.",
    "Канонічний цикл створення веб-платформи": "Canonical lifecycle of web platform creation",
    "Канонічний цикл створення веб-платформи від фундаменту до релізу": "Canonical lifecycle of web platform creation from foundation to release",
    "Непорушний серверний фундамент, транзакції та чиста бізнес-логіка": "Unbreakable server foundation, transactions, and clean business logic",
    "Візуальна довершеність, що перетворює темряву екрана на шедевр": "Visual perfection that transforms screen darkness into a masterpiece",
    "Термінове вигнання багів, очищення архітектури та прискорення БД": "Urgent bug exorcism, architecture purification, and DB acceleration",
    "Авторизація та Ролі": "Auth & Role System",
    "Особистий Кабінет": "Personal Cabinet",
    "Деплой та SSL-захист": "Deployment & SSL",
    "EF Core Міграції": "EF Core Migrations",
    "Патерни Strategy & Observer": "Strategy & Observer Patterns",
    "FluentValidation Ядра": "Core FluentValidation",
    "Авторські макети Figma": "Bespoke Figma Mockups",
    "Інтерактивна SVG-графіка": "Interactive SVG Graphics",
    "3D-паралакс ефекти": "3D Parallax Effects",
    "Адаптивна верстка": "Responsive Layout",
    "Усунення Exception-ів": "Exception Elimination",
    "Оптимізація SQL-індексів": "SQL Index Optimization",
    "Покриття валідаторами": "Validator Coverage",
    "Ліквідація техборгу": "Tech Debt Liquidation",
    "Термін виконання": "Estimated Duration",
    "Від 7 до 21 дня": "7 to 21 days",
    "Від 5 до 14 днів": "5 to 14 days",
    "Від 4 до 10 днів": "4 to 10 days",
    "Від 1 до 5 днів": "1 to 5 days",
    "Замовити цей обряд ✦": "Request This Ritual ✦",
    "Перейти в Кабінет →": "Go to Cabinet →",
    "Оракул Складності Ритуалу": "Ritual Complexity Oracle",
    "Вкажіть параметри вашого майбутнього проєкту, щоб миттєво розрахувати необхідний ранг архітектури та орієнтовну тривалість робіт.": "Specify the parameters of your future project to instantly calculate the required architectural rank and estimated duration.",
    "I. Масштаб системи": "I. System Scale",
    "Середній Храм": "Medium Sanctuary",
    "Каплиця (Landing / MVP)": "Chapel (Landing / MVP)",
    "Середній Храм (Web App)": "Medium Sanctuary (Web App)",
    "Величний Собор (Enterprise)": "Grand Cathedral (Enterprise)",
    "Малий (MVP)": "Small (MVP)",
    "Веб-платформа": "Web Platform",
    "II. Складність бази даних та логіки": "II. Database & Logic Complexity",
    "Реляційна PostgreSQL": "Relational PostgreSQL",
    "Базова схема": "Basic Schema",
    "Базова": "Basic",
    "Реляційна PostgreSQL + CRM": "Relational PostgreSQL + CRM",
    "Високонавантажений кластер": "Highload Cluster",
    "Highload Кластер": "Highload Cluster",
    "ПРОРОЦТВО ОРАКУЛА": "ORACLE PROPHECY",
    "Орденська Веб-Платформа (.NET 8 + Supabase)": "Order Web Platform (.NET 8 + Supabase)",
    "Мала Каплиця (Швидкий MVP Старт)": "Small Chapel (Rapid MVP Launch)",
    "Кафедральний Enterprise-Собор Високої Надійності": "High-Reliability Enterprise Cathedral",
    "Орієнтовний час": "Estimated Time",
    "10 – 14 днів": "10 – 14 days",
    "3 – 6 днів": "3 – 6 days",
    "21 – 35 днів": "21 – 35 days",
    "Архітектурний канон": "Architectural Canon",
    "Передати ці параметри у Сповідь →": "Submit Parameters to Confession →",

    // --- ПОРТФОЛІО ТА МОДАЛЬНІ ВІКНА ІНСПЕКЦІЇ (PORTFOLIO) ---
    "Портфоліо Ордену": "Order Portfolio",
    "Галерея завершених обрядів та освячених архітектур": "Gallery of completed rituals and sanctified architectures",
    "Усі діяння (6)": "All Deeds (6)",
    "Веб-Собори": "Web Cathedrals",
    "Backend & БД": "Backend & DB",
    "Сакральний UI/UX": "Sacred UI/UX",
    "Флагманський портал ІТ-ордену у неоготичному стилі з інтерактивним 3D-артефактом, системою рангів та живою кімнатою сповіді.": "Flagship neogothic IT Order portal featuring an interactive 3D artifact, rank progression system, and live confession room.",
    "Високонавантажена торговельна платформа з атомарними транзакціями кошика, патерном Strategy для знижок та миттєвими сповіщеннями.": "High-load e-commerce platform with atomic cart transactions, Strategy pattern discounts, and instant notifications.",
    "Інтерактивний лорний веб-світ із багаторівневим паралаксом, динамічним освітленням за курсором та інтегрованим кабінетом гравця.": "Interactive lore web universe with multi-layered parallax, dynamic cursor lighting, and an integrated player cabinet.",
    "Захищена платформа онлайн-консультацій із криптографічною згодою пацієнтів, ізоляцією даних та захистом від Vendor Lock-in.": "Secured telemedicine platform with cryptographic patient consent, row-level data isolation, and vendor lock-in mitigation.",
    "Високопродуктивне ядро обліку та диспетчеризації міських мереж із рекурсивними CTE-запитами, тригерами аудиту та індексацією.": "High-performance municipal utility telemetry core utilizing recursive CTE queries, audit triggers, and B-Tree indexing.",
    "Внутрішня система управління клієнтами, автоматичної канонізації спонсорів, живого чату сповідей та аналітики діянь за періодами.": "Internal CRM for parishioner management, automatic sponsor canonization, live confession chat, and period analytics.",
    "Стандартні корпоративні сайти виглядають шаблонно, а прямі виклики БД у контролерах унеможливлюють масштабування.": "Standard corporate websites look generic, while direct DB calls inside controllers make scaling impossible.",
    "Трирівнева Clean Architecture (Core, DAL, MVC) із патернами Strategy, Factory, Observer та інтерактивним 3D-вівтарем на чистій векторній графіці.": "Three-tier Clean Architecture (Core, DAL, MVC) with Strategy, Factory, and Observer patterns plus an interactive pure-SVG 3D altar.",
    "Втрата замовлень під час пікових навантажень та розсинхронізація складських залишків при одночасній оплаті.": "Order loss during peak traffic spikes and inventory desynchronization during concurrent checkouts.",
    "Впроваджено патерн Unit of Work із явними транзакціями BeginTransactionAsync / CommitTransactionAsync та пулінгом з'єднань PostgreSQL.": "Implemented the Unit of Work pattern with explicit BeginTransactionAsync / CommitTransactionAsync transactions and PostgreSQL connection pooling.",
    "Важкі растрові ілюстрації гальмували завантаження сторінки та виглядали розмито на Retina-екранах.": "Heavy raster illustrations slowed down page loading and appeared blurry on high-DPI Retina displays.",
    "Повний перехід на багатошарову параметричну SVG-графіку з 3D-паралаксом у реальному часі за координатами курсора.": "Full transition to multi-layered parametric SVG graphics with real-time 3D parallax driven by cursor coordinates.",
    "Суворі вимоги до конфіденційності медичних консультацій та ризик блокування проєкту у випадку зміни хмарного провайдера.": "Strict medical consultation privacy requirements and vendor lock-in risks when switching cloud providers.",
    "Чиста доменна модель за інверсією залежностей (DIP) з розмежуванням прав доступу на рівні рядків БД та криптографічною верифікацією.": "Clean domain model built on Dependency Inversion (DIP) with Row-Level Security (RLS) and cryptographic verification.",
    "Повільна побудова ієрархічних звітів по інженерних вузлах міста (понад 4.5 секунди на один запит).": "Slow generation of hierarchical reports across municipal utility nodes (over 4.5 seconds per single query).",
    "Спроектовано реляційну схему PostgreSQL із рекурсивними CTE, композитними B-Tree індексами та автоматичними тригерами.": "Engineered a relational PostgreSQL schema with recursive CTEs, composite B-Tree indexes, and automated audit triggers.",
    "Відсутність прозорої системи лояльності клієнтів та ручне адміністрування порушників без фіксації термінів блокування.": "Lack of a transparent client loyalty system and manual moderation of violators without expiration tracking.",
    "Автоматичний розрахунок титулів через патерн Strategy, канонізація спонсорів при 5+ закритих проєктах та система тимчасових банів з авто-зняттям.": "Automated rank calculation via the Strategy pattern, sponsor canonization at 5+ completed projects, and timed bans with auto-expiration.",
    "Час виконання запиту: з 4500 ms до 18 ms": "Query execution time: reduced from 4500 ms to 18 ms",
    "Автоматизація CRM: 100% | Живий чат сповідей: Polling 4s": "CRM Automation: 100% | Live Confession Chat: 4s Polling",
    "Frame Rate: 60 FPS | Завантаження першого екрана: 0.4s": "Frame Rate: 60 FPS | First Contentful Paint: 0.4s",
    "Архітектура": "Architecture",
    "Відгук ядра": "Core Latency",
    "Транзакції/хв": "Transactions/min",
    "Надійність": "Reliability",
    "Візуальний ранг": "Visual Rank",
    "Конверсія": "Conversion",
    "Безпека даних": "Data Security",
    "Доступність": "Availability",
    "Швидкість SQL": "SQL Speed",
    "x4.8 швидше": "x4.8 faster",
    "Цілісність": "Integrity",
    "Автоматизація": "Automation",
    "Захист вводу": "Input Guard",
    "Дослідити архітектуру": "Inspect Architecture",
    "Замовити →": "Order →",
    "Інтерактивний Стенд: До та Після Екзорцизму": "Interactive Bench: Before & After Exorcism",
    "Переміщуйте повзунок освячення коду, щоб побачити, як сирий моноліт із технічним боргом перетворюється на швидкодійну архітектуру Ордену.": "Slide the sanctification bar to see how a raw monolith with technical debt transforms into a high-speed Order architecture.",
    "0% (Грішний Легасі)": "0% (Sinful Legacy)",
    "Рівень освячення: 100%": "Sanctification Level: 100%",
    "100% (Канон Ордену)": "100% (Order Canon)",
    "Час відповіді БД (SQL Latency)": "DB Response Time (SQL Latency)",
    "Пулінг Supavisor + Індекси": "Supavisor Pooling + Indexes",
    "Кількість необроблених збоїв": "Unhandled Failures Count",
    "ВИКЛИК (ПОЧАТКОВИЙ ГРІХ)": "CHALLENGE (ORIGINAL SIN)",
    "РІШЕННЯ ОРДЕНУ": "ORDER SOLUTION",
    "ФРАГМЕНТ ОСВЯЧЕНОГО ЯДРА:": "SANCTIFIED CORE FRAGMENT:",
    "Закрити сувій": "Close Scroll",
    "Замовити аналогічну архітектуру →": "Order Similar Architecture →",

    // --- СТАТИСТИКА (STATISTICS) ---
    "Літопис Діянь": "Chronicles of Deeds",
    "Відкрита статистика Ордену •": "Public Order Statistics •",
    "За останні 7 днів": "Last 7 Days",
    "За останні 30 днів": "Last 30 Days",
    "За поточний рік": "Current Year",
    "За весь час": "All Time",
    "За весь час існування Ордену": "All-Time Order History",
    "7 днів": "7 Days",
    "30 днів": "30 Days",
    "За рік": "1 Year",
    "Весь час": "All Time",
    "Усього замовлень": "Total Orders",
    "Занесено у Священний Гримуар": "Recorded in the Sacred Grimoire",
    "Успішно закрито": "Completed Rituals",
    "Освячені релізи без багів": "Sanctified bug-free releases",
    "Активні ритуали": "Active Rituals",
    "Індекс спасіння": "Salvation Index",
    "Частка завершених проєктів": "Share of completed projects",
    "Баланс Статусів Ордену": "Order Status Balance",
    "Співвідношення завершених, активних та скасованих обрядів": "Ratio of completed, active, and cancelled rituals",
    "Виконано (Completed)": "Completed",
    "В роботі (InProgress)": "In Progress",
    "Очікують (Pending)": "Pending",
    "Скасовано (Cancelled)": "Cancelled",
    "Розподіл Ритуалів за Категоріями": "Rituals Breakdown by Category",
    "Наведіть або натисніть на категорію, щоб переглянути специфікацію напрямку": "Hover or click a category to inspect its domain specification",
    "Зведення Веб-Соборів (Standard)": "Web Cathedrals Construction (Standard)",
    "Архітектура Ядра та БД (Backend)": "Core & DB Architecture (Backend)",
    "Сакральний UI/UX Візуал (Design)": "Sacred UI/UX Visuals (Design)",
    "Екзорцизм Легасі-Коду (Refactoring)": "Legacy Code Exorcism (Refactoring)",
    "Комплексні корпоративні платформи та веб-застосунки повного циклу з особистими кабінетами.": "Full-cycle enterprise platforms and web applications with personal user cabinets.",
    "Високонадійні серверні ядра на .NET 8, Clean Architecture, транзакції Unit of Work та кластери PostgreSQL.": "High-reliability .NET 8 server cores, Clean Architecture, Unit of Work transactions, and PostgreSQL clusters.",
    "Авторські неоготичні інтерфейси, векторна графіка, 3D-паралакс сцени та дизайн-системи.": "Bespoke neogothic interfaces, vector graphics, 3D parallax scenes, and design systems.",
    "Термінове вигнання критичних багів, оптимізація повільних SQL-запитів та усунення технічного боргу.": "Urgent exorcism of critical bugs, slow SQL query optimization, and technical debt elimination.",

    // --- СПОНСОРИ (SPONSORS) ---
    "Пантеон Святих Спонсорів": "Pantheon of Holy Sponsors",
    "Меценати та фундатори Ордену Багоугодних": "Patrons and Founders of the Order",
    "До цього священного залу автоматично входять парафіяни, які успішно закрили": "This sacred hall automatically welcomes parishioners who have completed",
    "5+ проєктів": "5+ projects",
    ", а також почесні благодійники, висвячені Верховним Інквізитором.": ", as well as honorary benefactors ordained by the High Inquisitor.",
    "Канонізувати існуючого учасника": "Canonize Existing Member",
    "-- Оберіть парафіянина з бази --": "-- Select parishioner from database --",
    "Надати титул Святого Спонсора ✦": "Grant Holy Sponsor Title ✦",
    "Вписати нового Святого Спонсора": "Inscribe New Holy Sponsor",
    "+ Додати в Пантеон": "+ Add to Pantheon",
    "Пантеон очікує на перших Меценатів": "The Pantheon Awaits Its First Patrons",
    "Замовте та успішно завершіть 5 обрядів розробки, щоб ваше ім'я було викарбувано тут у золоті.": "Order and complete 5 development rituals to have your name carved here in gold.",
    "Закрито проєктів": "Completed Projects",
    "В Ордені з": "In the Order Since",
    "Зняти почесний статус": "Revoke Honorary Status",

    // --- КАБІНЕТ ТА КІМНАТА СПОВІДІ (CABINET & DETAILS ) ---
    "Усі замовлення Ордену": "All Order Scrolls",
    "Мої замовлення": "My Orders",
    "Святилище Профілю": "Profile Sanctuary",
    "Замовити новий обряд": "Request New Ritual",
    "Гримуар Клієнтів (CRM)": "Client Grimoire (CRM)",
    "Літопис Статистики": "Statistics Chronicle",
    "Вийти з Храму": "Leave Sanctuary",
    "Найвищий ранг досягнуто": "Highest Rank Achieved",
    "УСЬОГО ЗАЯВОК": "TOTAL ORDERS",
    "АКТИВНІ РИТУАЛИ": "ACTIVE RITUALS",
    "УСПІШНО ЗАДЕПЛОЄНО": "SUCCESSFULLY DEPLOYED",
    "Архів Усіх Сповідей Ордену": "Archive of All Order Confessions",
    "Сувої Ваших Замовлень": "Scrolls of Your Orders",
    "Керування параметрами обрядів та прямий перехід у Кімнату Сповіді": "Manage ritual parameters and access the Confession Room directly",
    "+ Новий обряд ✦": "+ New Ritual ✦",
    "У Священному Гримуарі ще не зафіксовано жодної заявки.": "No order scrolls have been recorded in the Sacred Grimoire yet.",
    "Ініціювати перший обряд ✦": "Initiate First Ritual ✦",
    "В роботі": "In Progress",
    "Очікують": "Pending",
    "Завершені": "Completed",
    "Очікує ритуалу": "Awaiting Ritual",
    "Завершено": "Completed",
    "Скасовано": "Cancelled",
    "Сповідь": "Confession",
    "Ред.": "Edit",
    "Видалити": "Delete",
    "I. Очікування": "I. Pending",
    "II. В роботі": "II. In Progress",
    "III. Освячено": "III. Sanctified",
    "Парафіянин": "Parishioner",
    "Інквізитор": "Inquisitor",
    "Категорія Ритуалу": "Ritual Category",
    "СУВІЙ ОПИСУ ГРІХА (ТЕХНІЧНЕ ЗАВДАННЯ):": "SCROLL OF SIN DESCRIPTION (SPECIFICATION):",
    "Редагувати заявку": "Edit Order",
    "Видалити заявку": "Delete Order",
    "Воля Інквізитора (Зміна статусу обряду):": "Inquisitor's Will (Change Ritual Status):",
    "Pending (В очікуванні)": "Pending (Awaiting)",
    "InProgress (В роботі)": "InProgress (In Progress)",
    "Completed (Виконано +1 до рангу)": "Completed (+1 to Rank)",
    "Cancelled (Скасовано)": "Cancelled",
    "Вердикт": "Verdict",
    "← Повернутися до Кабінету Замовлень": "← Back to Orders Cabinet",
    "Кімната Сповіді": "Confession Room",
    "Захищений канал зв'язку між Парафіянином та Інквізицією Ордену": "Secured communication channel between Parishioner and the Order Inquisition",
    "У Кімнаті Сповіді поки панує тиша. Напишіть перше послання...": "Silence reigns in the Confession Room. Write the first message...",
    "Зберегти": "Save",
    "+ Прийнято в роботу": "+ Accepted for Work",
    "+ Запит деталей БД": "+ Request DB Details",
    "+ Реліз готовий": "+ Release Ready",
    "+ Запитати статус": "+ Ask for Status",
    "+ Додати уточнення": "+ Add Clarification",
    "Надіслати ✦": "Send ✦",
    "Захищено інспектором FluentValidation": "Protected by FluentValidation Inspector",
    "[UI/UX] Чистий дизайн": "[UI/UX] Pure Design",
    "Чистий дизайн": "Pure Design",
    "Потрібна розробка веб-системи на .NET 8 із підключенням бази даних PostgreSQL та особистим кабінетом.": "Need development of a .NET 8 web system with PostgreSQL database integration and a personal cabinet.",
    "Необхідно спроєктувати REST API, схему таблиць БД та бізнес-логіку за Clean Architecture.": "Need to design a REST API, database table schema, and Clean Architecture business logic.",
    "Потрібен редизайн інтерфейсу, інтерактивна SVG-графіка та адаптивна верстка сторінок.": "Need an interface redesign, interactive SVG graphics, and responsive page layouts.",
    "Вітаю! Чи є новини щодо мого обряду?": "Greetings! Are there any updates regarding my ritual?",
    "Сповідь прийнято. Розпочинаємо архітектурний аналіз.": "Confession accepted. Initiating architectural analysis.",
    "Уточніть, будь ласка, деталі схеми бази даних та вимоги до API.": "Please clarify the database schema details and API requirements.",
    "Екзорцизм багів завершено. Перевірте оновлений реліз.": "Bug exorcism completed. Please verify the updated release.",

    // --- СТВОРЕННЯ, РЕДАГУВАННЯ, ГРИМУАР, ПРОФІЛЬ, ВХІД, БАН ---
    "Ініціація Нового Обряду": "Initiation of a New Ritual",
    "Сформуйте технічний сувій, щоб Жреці Коду розпочали зведення вашої архітектури": "Compose the technical scroll so the Priests of Code may begin forging your architecture",
    "I. Оберіть Категорію Таїнства": "I. Select Sacrament Category",
    "I. Категорія Таїнства": "I. Sacrament Category",
    "Веб-Собор під ключ": "Turnkey Web Cathedral",
    "Standard • Повний цикл": "Standard • Full Cycle",
    "Standard • Розробка з нуля": "Standard • From Scratch",
    "Ядро та Бази Даних": "Core & Databases",
    "Backend • .NET & SQL": "Backend • .NET & SQL",
    "Backend • Архітектура .NET та БД": "Backend • .NET & DB Architecture",
    "Сакральний Візуал": "Sacred Visuals",
    "Design • Авто-префікс [UI/UX]": "Design • Auto-prefix [UI/UX]",
    "Design • Готичний UI/UX дизайн": "Design • Neogothic UI/UX Design",
    "Екзорцизм Багів": "Bug Exorcism",
    "Refactoring • Терміновий": "Refactoring • Urgent",
    "II. Назва Обряду (5 – 100 символів)": "II. Ritual Title (5 – 100 chars)",
    "II. Назва Проєкту (5 – 100 символів)": "II. Project Title (5 – 100 chars)",
    "III. Технічний Сувій та Опис Проблеми": "III. Technical Scroll & Problem Description",
    "III. Опис Завдання": "III. Task Description",
    "+ Шаблон .NET + PostgreSQL": "+ .NET + PostgreSQL Template",
    "+ Шаблон Backend Ядра": "+ Backend Core Template",
    "+ Шаблон UI/UX Дизайну": "+ UI/UX Design Template",
    "Молитися і Задеплоїти": "Pray & Deploy",
    "Молитися і Оновити Деплой": "Pray & Update Deploy",
    "Скасувати": "Cancel",
    "Сувій Перед Деплоєм": "Pre-Deployment Scroll",
    "Оновлений Сувій": "Updated Scroll",
    "Так ваша заявка виглядатиме у Гримуарі після обробки патерном": "How your order will appear in the Grimoire after",
    "Попередній перегляд сувою після форматування фабрикою": "Scroll preview after formatting by",
    "Очікування назви обряду...": "Awaiting ritual title...",
    "Опис технічного завдання з'явиться тут...": "Technical specification description will appear here...",
    "КАНОНИ БІЗНЕС-ВАЛІДАТОРА (ORDERVALIDATOR):": "BUSINESS VALIDATOR CANONS (ORDERVALIDATOR):",
    "ПЕРЕВІРКА КАНОНІВ (ORDERVALIDATOR):": "CANON VERIFICATION (ORDERVALIDATOR):",
    "Довжина назви від 5 до 100 осмислених символів": "Title length between 5 and 100 meaningful characters",
    "Спокійний тон без крику (вимкнено Caps Lock)": "Calm tone without shouting (Caps Lock off)",
    "Для Backend вказано контекст (БД, SQL, API, .NET, сервер)": "Backend context specified (DB, SQL, API, .NET, server)",
    "← Повернутися до Кабінету без збереження": "← Return to Cabinet without saving",
    "Редагування Заявки": "Edit Order Scroll",
    "Внесіть зміни у параметри сувою": "Modify the parameters of scroll",
    "перед повторним освяченням": "before re-sanctification",
    "Гримуар Клієнтів": "Client Grimoire",
    "Реєстр парафіян, канонізація спонсорів та Трибунал Інквізиції": "Parishioner registry, sponsor canonization, and Inquisition Tribunal",
    "← До архіву заявок": "← Back to Orders Archive",
    "Усього у Гримуарі": "Total in Grimoire",
    "Святі Спонсори": "Holy Sponsors",
    "Активні Парафіяни": "Active Parishioners",
    "Під Анафемою (Бан)": "Under Anathema (Banned)",
    "Інквізиція": "Inquisition",
    "Ім'я / Електронна пошта": "Name / Email",
    "Титул в Ордені": "Order Title",
    "Статус та Вирок": "Status & Verdict",
    "Діяння (Замовлення)": "Deeds (Orders)",
    "Суд Інквізиції": "Inquisition Tribunal",
    "АНАФЕМА (БЛОКУВАННЯ)": "ANATHEMA (BANNED)",
    "Активний": "Active",
    "Зняти спонсора": "Revoke Sponsor",
    "Спонсор ✦": "Sponsor ✦",
    "Помилувати": "Pardon",
    "Анафема (Бан)": "Anathema (Ban)",
    "+ Спам у сповіді": "+ Confession Spam",
    "+ Спроба ін’єкції": "+ Injection Attempt",
    "+ Порушення канонів": "+ Canon Violation",
    "На 1 годину": "For 1 Hour",
    "На 24 години (1 доба)": "For 24 Hours (1 Day)",
    "На 3 доби": "For 3 Days",
    "На 7 днів": "For 7 Days",
    "На 30 днів": "For 30 Days",
    "Назавжди (Довічно)": "Permanent (Lifetime)",
    "Винести вирок": "Pass Verdict",
    "Управління особистими реквізитами, ключами доступу та присягою Ордену": "Manage personal credentials, access keys, and your Order oath",
    "← Повернутися в Кабінет": "← Return to Cabinet",
    "Досягнуто найвищого сану Пантеону": "Highest Pantheon Rank Achieved",
    "Особисті Реквізити": "Personal Credentials",
    "Зміна публічного імені у чатах та пошти": "Update your public chat name and email",
    "Ім'я або Псевдонім в Ордені": "Name or Alias in the Order",
    "3–50 символів": "3–50 chars",
    "Електронна пошта (Email)": "Email Address",
    "Логін для входу": "Login Credential",
    "Після збереження ваша сесія та ім'я у верхній шапці Храму оновляться автоматично.": "After saving, your session and header name will update automatically.",
    "Зберегти нові реквізити ✦": "Save New Credentials ✦",
    "Зміна Таємного Пароля": "Change Secret Password",
    "Оновлення криптографічного ключа SHA-256": "Update your SHA-256 cryptographic key",
    "Поточний пароль": "Current Password",
    "Новий пароль (мінімум 6 символів)": "New Password (min. 6 chars)",
    "Очікування...": "Waiting...",
    "Підтвердіть новий пароль": "Confirm New Password",
    "Оновити таємний пароль": "Update Secret Password",
    "Зречення Присяги (Спалення Профілю)": "Renounce Oath (Burn Profile)",
    "Видалення профілю є остаточним і незворотним. Усі ваші активні та архівні замовлення, а також сувої повідомлень у Кімнатах Сповіді будуть назавжди стерті з хронік Ордену.": "Profile deletion is final and irreversible. All your active and archived orders and Confession Room messages will be permanently erased from the Order chronicles.",
    "Я усвідомлюю незворотність знищення моїх сувоїв": "I understand the irreversibility of destroying my scrolls",
    "Спалити профіль": "Burn Profile",
    "Вхід в Орден": "Enter the Order",
    "Пред'явіть ваші ключі доступу перед Брамою Храму": "Present your access keys before the Sanctuary Gate",
    "Швидка Печатка Інквізиції (Автозаповнення)": "Quick Inquisition Seal (Auto-Fill)",
    "Жрець Коду": "Priest of Code",
    "Інквізиторка": "Inquisitor",
    "Таємний пароль": "Secret Password",
    "Запам'ятати мою печатку на 14 днів": "Remember my seal for 14 days",
    "Увійти в Кабінет ✦": "Enter Cabinet ✦",
    "Ще не прийняли присягу?": "Have not taken the oath yet?",
    "Прийняти Присягу Парафіянина →": "Take the Parishioner Oath →",
    "Новий Парафіянин": "New Parishioner",
    "Прийміть присягу Ордену, щоб ініціювати обряди та спілкуватися з Інквізицією": "Take the Order oath to initiate rituals and communicate with the Inquisition",
    "Новачок Ордену": "Order Neophyte",
    "Постійний парафіянин": "Devout Parishioner",
    "Магістр Ордену • Святий Спонсор": "Grandmaster • Holy Sponsor",
    "Почесний Спонсор Ордену": "Honorary Order Sponsor",
    "Ваше ім'я або назва компанії": "Your Name or Company Name",
    "Логін у Храмі": "Sanctuary Login",
    "Таємний пароль (мін. 6 символів)": "Secret Password (min. 6 chars)",
    "Повторіть пароль": "Repeat Password",
    "Прийняти Присягу ✦": "Take the Sacred Oath ✦",
    "Вже маєте печатку Ордену?": "Already possess an Order seal?",
    "Увійти в Храм →": "Enter Sanctuary →",
    "На вас накладено Анафему": "Anathema Has Been Cast Upon You",
    "Трибунал Інквізиції обмежив доступ до святилища для акаунта": "The Inquisition Tribunal has restricted sanctuary access for account",
    "ОФІЦІЙНИЙ ВИРОК ТРИБУНАЛУ": "OFFICIAL TRIBUNAL VERDICT",
    "ТЕРМІН ВИГНАННЯ: ДОВІЧНА АНАФЕМА (БЕЗСТРОКОВО)": "EXILE DURATION: PERMANENT ANATHEMA (INDEFINITE)",
    "Тільки Верховний Інквізитор Ордену може дарувати вам помилування та зняти печатку відлучення.": "Only the High Inquisitor of the Order may grant you pardon and lift the seal of excommunication.",
    "Спокута гріхів триватиме до:": "Penance shall last until:",
    "Днів": "Days",
    "Годин": "Hours",
    "Хвилин": "Minutes",
    "Секунд": "Seconds",
    "Повернутися на Головну": "Return to Home",
    "Увійти під іншою печаткою →": "Login with Another Seal →",
    "Виявлено Демонічну Аномалію": "Demonic Anomaly Detected",
    "Під час виконання вашого запиту стався непередбачений збій, але захисний бар'єр Ордену вже ізолював загрозу.": "An unexpected fault occurred while processing your request, but the Order's protective barrier has isolated the threat.",
    "СЛІД АНОМАЛІЇ (REQUEST TRACE ID):": "ANOMALY TRACE (REQUEST TRACE ID):",
    "Скопіювати ID": "Copy ID",
    "Запустити діагностику ядра": "Run Core Diagnostics",
    "Повернутися на Головну ✦": "Return to Home ✦",
    "До Особистого Кабінету →": "To Personal Cabinet →",

    "// Освячення архітектури перед деплоєм": "// Sanctifying architecture before deployment",
    "// Пул захищених з'єднань із Храмом Даних": "// Secured connection pool to the Data Sanctuary",
    "/* Золотий перетин та сакральне світіння */": "/* Golden ratio and sacred luminescence */",
    "// Вигнання легасі-демонів із критичного модуля": "// Exorcising legacy daemons from critical module"
};

function translateDynamicPattern(str, lang) {
    if (lang !== 'en') return null;

    let m = str.match(/^(?:🔑\s*)?З поверненням у Храм Коду,\s*(.+)!$/);
    if (m) return `✦ Welcome back to the Sanctuary of Code, ${fullTextMap[m[1]] || m[1]}!`;

    m = str.match(/^(?:✨\s*)?Присягу прийнято! Вітаємо в Ордені,\s*(.+)!$/);
    if (m) return `✦ Oath accepted! Welcome to the Order, ${fullTextMap[m[1]] || m[1]}!`;

    m = str.match(/^(?:✨\s*)?Обряд «(.+)» успішно прийнято в роботу!$/);
    if (m) return `✦ Ritual “${fullTextMap[m[1]] || m[1]}” has been accepted!`;

    m = str.match(/^✦\s*ПРОПУЩЕНІ ПОВІДОМЛЕННЯ\s*\((\d+)\)$/);
    if (m) return `✦ MISSED MESSAGES (${m[1]})`;

    if (str === 'У чаті') return 'In chat';
    m = str.match(/^від\s+(.+)$/);
    if (m) return `from ${fullTextMap[m[1]] || m[1]}`;

    m = str.match(/^(\d+)\s+замовлень\s+\((\d+)%\)$/);
    if (m) return `${m[1]} orders (${m[2]}%)`;

    m = str.match(/^В роботі:\s*(\d+)\s*•\s*В черзі:\s*(\d+)$/);
    if (m) return `In Progress: ${m[1]} • Pending: ${m[2]}`;

    m = str.match(/^Усі\s*\((\d+)\)$/);
    if (m) return `All (${m[1]})`;

    m = str.match(/^Усі душі\s*\((\d+)\)$/);
    if (m) return `All Souls (${m[1]})`;

    m = str.match(/^Святі Спонсори\s*\((\d+)\)$/);
    if (m) return `Holy Sponsors (${m[1]})`;

    m = str.match(/^Під Анафемою\s*\((\d+)\)$/);
    if (m) return `Under Anathema (${m[1]})`;

    m = str.match(/^Непрочитані\s*\((\d+)\)$/);
    if (m) return `Unread (${m[1]})`;

    m = str.match(/^Рівень освячення:\s*(\d+)%$/);
    if (m) return `Sanctification Level: ${m[1]}%`;

    m = str.match(/^Опис містить щонайменше (\d+) символів \(зараз:\s*(\d+)\)$/);
    if (m) return `Description has at least ${m[1]} chars (now: ${m[2]})`;

    m = str.match(/^До титулу «(.+)»$/);
    if (m) return `Towards rank “${fullTextMap[m[1]] || m[1]}”`;

    m = str.match(/^Прогрес до «(.+)»:\s*(\d+)\/(\d+)$/);
    if (m) return `Progress to “${fullTextMap[m[1]] || m[1]}”: ${m[2]}/${m[3]}`;

    m = str.match(/^В Ордені з\s+(.+)$/);
    if (m) return `In the Order since ${m[1]}`;

    m = str.match(/^Закрито:\s*(\d+)$/);
    if (m) return `Completed: ${m[1]}`;

    m = str.match(/^Причина:\s*(.+)$/);
    if (m) return `Reason: ${m[1]}`;

    m = str.match(/^Строк:\s*(.+)$/);
    if (m) return `Duration: ${m[1].replace('Довічно', 'Permanent').replace('до ', 'until ')}`;

    m = str.match(/^Статус:\s*(.+)$/);
    if (m) return `Status: ${m[1]}`;

    m = str.match(/^ОБРЯД\s+(.+)$/);
    if (m) return `RITUAL ${m[1]}`;

    return null;
}

const placeholderMap = {
    "Пошук за #PD або назвою...": "Search by #PD or title...",
    "Пошук за ім'ям, Email або титулом...": "Search by name, Email, or title...",
    "Впишіть ваше послання у Кімнату Сповіді...": "Write your message in the Confession Room...",
    "Ім'я або Компанія": "Name or Company",
    "Email мецената": "Patron Email",
    "Наприклад: Розробка корпоративного порталу з CRM...": "E.g.: Enterprise Portal Development with CRM...",
    "Опишіть архітектурні вимоги, стек технологій (БД, SQL, API, .NET), потрібні сторінки або симптоми багів...": "Describe architectural requirements, tech stack (DB, SQL, API, .NET), pages, or bug symptoms...",
    "Вкажіть офіційну причину вироку...": "Specify the official verdict reason...",
    "Поточний пароль...": "Current password..."
};

/* --- 1. ТЕМА (DARK / LIGHT) --- */
function syncThemeIcon() {
    const theme = document.documentElement.getAttribute('data-theme') || 'dark';
    const moon = document.getElementById('themeIconMoon');
    const sun = document.getElementById('themeIconSun');
    if (moon && sun) {
        moon.style.display = theme === 'dark' ? 'block' : 'none';
        sun.style.display = theme === 'light' ? 'block' : 'none';
    }
}

function toggleSacredTheme() {
    const current = document.documentElement.getAttribute('data-theme') || 'dark';
    const next = current === 'dark' ? 'light' : 'dark';
    document.documentElement.setAttribute('data-theme', next);
    localStorage.setItem('holy-theme', next);
    syncThemeIcon();
}

/* --- 2. МОВА (UA / EN) --- */
function applySacredLanguage(lang) {
    document.documentElement.setAttribute('data-lang', lang);
    document.documentElement.lang = lang;
    localStorage.setItem('holy-lang', lang);

    const lbl = document.getElementById('langSwitchLabel');
    if (lbl) lbl.textContent = lang === 'uk' ? 'UA' : 'EN';

    const dict = i18nDict[lang] || i18nDict.uk;
    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.getAttribute('data-i18n');
        if (dict[key] && el.innerHTML !== dict[key]) {
            el.innerHTML = dict[key];
        }
    });

    const walker = document.createTreeWalker(
        document.body,
        NodeFilter.SHOW_TEXT,
        {
            acceptNode(node) {
                const p = node.parentElement;
                if (!p) return NodeFilter.FILTER_REJECT;
                const tag = p.tagName;
                if (tag === 'SCRIPT' || tag === 'STYLE') {
                    return NodeFilter.FILTER_REJECT;
                }
                if (p.id === 'eyeTelemetry' || p.closest('[data-i18n]')) {
                    return NodeFilter.FILTER_REJECT;
                }
                return NodeFilter.FILTER_ACCEPT;
            }
        }
    );

    const textNodes = [];
    while (walker.nextNode()) textNodes.push(walker.currentNode);

    textNodes.forEach(node => {
        const raw = node.nodeValue;
        const trimmed = raw.trim().replace(/\s+/g, ' ');
        if (!trimmed) return;

        if (node.parentElement && node.parentElement.classList.contains('ritual-code-preview')) {
            if (!node._origUkCode) node._origUkCode = raw;
            if (lang === 'en') {
                let codeEn = node._origUkCode;
                Object.keys(fullTextMap).forEach(k => {
                    if (k.startsWith('//') || k.startsWith('/*')) {
                        codeEn = codeEn.replace(k, fullTextMap[k]);
                    }
                });
                node.nodeValue = codeEn;
            } else {
                node.nodeValue = node._origUkCode;
            }
            return;
        }

        if (node._origUk === undefined || fullTextMap[trimmed] || translateDynamicPattern(trimmed, 'en')) {
            node._origUk = raw;
            node._origTrimmed = trimmed;
        }

        if (lang === 'en') {
            const exact = fullTextMap[node._origTrimmed];
            const dyn = exact ? null : translateDynamicPattern(node._origTrimmed, 'en');
            const translated = exact || dyn;
            if (translated) {
                const lead = node._origUk.match(/^\s*/)[0];
                const trail = node._origUk.match(/\s*$/)[0];
                const nextVal = lead + translated + trail;
                if (node.nodeValue !== nextVal) {
                    node.nodeValue = nextVal;
                }
            }
        } else if (node._origUk !== undefined && node.nodeValue !== node._origUk) {
            node.nodeValue = node._origUk;
        }
    });

    document.querySelectorAll('input[placeholder], textarea[placeholder]').forEach(inp => {
        const ph = inp.getAttribute('placeholder');
        if (!inp.getAttribute('data-orig-ph')) {
            inp.setAttribute('data-orig-ph', ph);
        }
        const origPh = inp.getAttribute('data-orig-ph');
        if (lang === 'en' && placeholderMap[origPh]) {
            inp.setAttribute('placeholder', placeholderMap[origPh]);
        } else if (lang === 'uk' && origPh) {
            inp.setAttribute('placeholder', origPh);
        }
    });
}

function toggleSacredLanguage() {
    const current = document.documentElement.getAttribute('data-lang') || 'uk';
    const next = current === 'uk' ? 'en' : 'uk';
    applySacredLanguage(next);
}

/* --- 3. ІНІЦІАЛІЗАЦІЯ ТА ПОДІЇ --- */
function toggleUserDropdown(e) {
    e.stopPropagation();
    const wrap = document.getElementById('userDropdownWrap');
    if (wrap) wrap.classList.toggle('open');
}

document.addEventListener('mousemove', (e) => {
    document.documentElement.style.setProperty('--mouse-x', `${e.clientX}px`);
    document.documentElement.style.setProperty('--mouse-y', `${e.clientY}px`);
});

document.addEventListener('click', (e) => {
    const wrap = document.getElementById('userDropdownWrap');
    if (wrap && !wrap.contains(e.target)) {
        wrap.classList.remove('open');
    }

    if (!e.target.closest('#langSwitchBtn')) {
        const currentLang = document.documentElement.getAttribute('data-lang') || 'uk';
        if (currentLang === 'en') {
            setTimeout(() => applySacredLanguage('en'), 30);
        }
    }
});

document.addEventListener('DOMContentLoaded', () => {
    syncThemeIcon();
    const savedLang = localStorage.getItem('holy-lang') || 'uk';
    applySacredLanguage(savedLang);

    setTimeout(() => {
        document.querySelectorAll('.holy-toast').forEach(t => {
            t.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            t.style.opacity = '0';
            t.style.transform = 'translateX(30px)';
            setTimeout(() => t.remove(), 500);
        });
    }, 5000);
});