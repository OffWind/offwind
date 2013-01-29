urls = (
    '/parse', 'handlers.parse',
    '/list/(.+)', 'handlers.list',
    '/plot/(.+)/(.+)', 'handlers.plot',
    '/read/(.+)/(.+)/(.+)', 'handlers.read',
    '(.*)', 'handlers.hello',
    '/(.*)', 'handlers.hello'
)
