# Configuración de Serenity Backend

## 📋 Requisitos Previos

1. .NET 9.0 SDK
2. PostgreSQL (local o remoto)
3. Cuenta de Groq AI (para funcionalidades de IA)
4. Credenciales de Google OAuth (para login con Google)

## 🔧 Configuración Inicial

### 1. Copiar archivo de configuración

Copia el archivo de ejemplo y renómbralo:

```bash
cp appsettings.example.json appsettings.json
```

### 2. Configurar Base de Datos PostgreSQL

Edita `appsettings.json` y actualiza la cadena de conexión:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=TU_HOST;Port=5432;Database=TU_DATABASE;Username=TU_USUARIO;Password=TU_PASSWORD;SSL Mode=Require;"
}
```

**Ejemplo para Render:**
```
Host=TU_HOST;Port=5432;Database=TU_DATABASE;Username=TU_USUARIO;Password=TU_PASSWORD;SSL Mode=Require;
```

### 3. Configurar JWT

Genera una clave secreta segura (mínimo 32 caracteres):

```json
"Jwt": {
  "SecretKey": "TU_CLAVE_SECRETA_DEBE_TENER_AL_MENOS_32_CARACTERES",
  "Issuer": "Serenity",
  "Audience": "SerenityUsers",
  "ExpirationMinutes": 60
}
```

**Generar clave secreta:**
```bash
# En PowerShell
-join ((65..90) + (97..122) + (48..57) | Get-Random -Count 32 | % {[char]$_})

# En Linux/Mac
openssl rand -base64 32
```

### 4. Configurar Groq AI

1. Crea una cuenta en [Groq Console](https://console.groq.com/)
2. Genera una API Key
3. Actualiza la configuración:

```json
"Groq": {
  "ApiKey": "gsk_TU_API_KEY_AQUI",
  "BaseUrl": "https://api.groq.com/openai/v1",
  "Model": "llama-3.1-8b-instant"
}
```

**Modelos disponibles:**
- `llama-3.1-8b-instant` (rápido, recomendado)
- `llama-3.3-70b-versatile` (más potente)

### 5. Configurar Google OAuth

1. Ve a [Google Cloud Console](https://console.cloud.google.com/)
2. Crea un proyecto o selecciona uno existente
3. Habilita Google+ API
4. Crea credenciales OAuth 2.0
5. Configura URIs de redirección autorizados
6. Actualiza la configuración:

```json
"Authentication": {
  "Google": {
    "ClientId": "TU_CLIENT_ID.apps.googleusercontent.com",
    "ClientSecret": "TU_CLIENT_SECRET"
  }
}
```

## 🗄️ Migraciones de Base de Datos

### Aplicar migraciones

```bash
dotnet ef database update --project serenity.Infrastructure --startup-project serenity
```

### Crear nueva migración

```bash
dotnet ef migrations add NombreMigracion --project serenity.Infrastructure --startup-project serenity
```

## 🚀 Ejecutar la Aplicación

```bash
dotnet run --project serenity
```

La aplicación estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `http://localhost:5000` (en desarrollo)

## 📝 Variables de Configuración

| Variable | Descripción | Requerido |
|----------|-------------|-----------|
| `ConnectionStrings:DefaultConnection` | Cadena de conexión a PostgreSQL | ✅ |
| `Jwt:SecretKey` | Clave secreta para firmar tokens JWT | ✅ |
| `Jwt:Issuer` | Emisor del token JWT | ✅ |
| `Jwt:Audience` | Audiencia del token JWT | ✅ |
| `Groq:ApiKey` | API Key de Groq AI | ⚠️ (solo para IA) |
| `Authentication:Google:ClientId` | Google OAuth Client ID | ⚠️ (solo para login Google) |
| `Authentication:Google:ClientSecret` | Google OAuth Client Secret | ⚠️ (solo para login Google) |

## 🔒 Seguridad

⚠️ **IMPORTANTE:**
- Nunca subas `appsettings.json` al repositorio
- Usa variables de entorno en producción
- Rota las claves secretas periódicamente
- Usa HTTPS en producción

## 📚 Documentación de la API

Una vez que la aplicación esté ejecutándose, accede a:
- Swagger UI: `http://localhost:5000`
- Documentación OpenAPI: `http://localhost:5000/swagger/v1/swagger.json`

## 🆘 Solución de Problemas

### Error de conexión a la base de datos
- Verifica que PostgreSQL esté ejecutándose
- Confirma que la cadena de conexión sea correcta
- Verifica que el usuario tenga permisos

### Error de autenticación JWT
- Verifica que `SecretKey` tenga al menos 32 caracteres
- Confirma que `Issuer` y `Audience` coincidan

### Error con Groq AI
- Verifica que la API Key sea válida
- Confirma que tengas créditos disponibles
- Revisa los límites de rate limiting

