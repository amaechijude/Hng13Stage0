### Hng13Stage0

Minimal ASP.NET Core (.NET 9) web API with a `/me` endpoint that returns a random cat fact.

### Endpoints

- **GET** `/me`: returns JSON like:
```json
{ "fact": "Cats have five toes on their front paws..." }
```
- **Dev only** (when `ASPNETCORE_ENVIRONMENT=Development`): OpenAPI at `/openapi/v1.json`

### Prerequisites

- **.NET SDK 9.0**
  - Windows/macOS: get from `https://dotnet.microsoft.com/download/dotnet/9.0`
  - Verify: `dotnet --version` (should show 9.x)
- **Docker** (optional, for containerized run)
  - Install Docker Desktop: `https://www.docker.com/products/docker-desktop/`
- **Git** (optional for cloning)
  - `https://git-scm.com/downloads`

### Clone

```bash
git clone https://github.com/amaechijude/Hng13Stage0.git
cd Hng13Stage0
```

### Run locally (bare metal)

From the project directory `Hng13Stage0/`:

```bash
dotnet restore
dotnet build
dotnet run
```

By default (per `Hng13Stage0/Properties/launchSettings.json`):
- HTTP: `http://localhost:5097`
- HTTPS: `https://localhost:7102`

If you want to use HTTPS locally, ensure dev certs are trusted:
```bash
dotnet dev-certs https --trust
```

#### Test the /me endpoint

- HTTP:
```bash
curl http://localhost:5097/me
```

- HTTPS (if dev certs trusted):
```bash
curl https://localhost:7102/me
```

- PowerShell:
```powershell
Invoke-RestMethod -Uri http://localhost:5097/me
```

- Browser: open `http://localhost:5097/me`

- OpenAPI (Development only): `http://localhost:5097/openapi/v1.json`

### Run with Docker

Build the image from the repo root (Dockerfile is at `Hng13Stage0/Dockerfile`):

```bash
docker build -f Hng13Stage0/Dockerfile -t hng13stage0 .
```

Run the container (HTTP only):

```bash
docker run --rm -p 8080:8080 hng13stage0
```

Note:
- The image exposes `8080` (HTTP) and `8081` (HTTPS). Unless you set up an HTTPS cert inside the container, prefer HTTP only. Avoid setting `ASPNETCORE_HTTPS_PORTS` if you haven’t mounted a cert.

Test from host:

```bash
curl http://localhost:8080/me
```

### Environment variables

- **ASPNETCORE_ENVIRONMENT**: defaults to `Production`. Use `Development` to enable OpenAPI.
- **ASPNETCORE_URLS**: override Kestrel bindings, e.g. `http://localhost:5097;https://localhost:7102`.
- Container-specific (only if you know what you’re doing with HTTPS):
  - **ASPNETCORE_HTTP_PORTS**: set container HTTP port, e.g. `8080`.
  - **ASPNETCORE_HTTPS_PORTS**: set container HTTPS port, e.g. `8081` (requires valid cert inside container).

If you want to force a single HTTP port locally (no HTTPS), you can run:
```bash
ASPNETCORE_URLS=http://localhost:5097 dotnet run
```
(Windows PowerShell: `$Env:ASPNETCORE_URLS='http://localhost:5097'; dotnet run`)

### Dependencies

- Project targets `net9.0` (`.NET 9 SDK`)
- NuGet packages:
  - `Microsoft.AspNetCore.OpenApi` (9.0.9) – generates OpenAPI docs
  - `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` – VS Docker tooling support

Install/restore packages via:
```bash
dotnet restore
```

### Notes

- CORS is open (`AllowAnyOrigin/Method/Header`).
- `/me` fetches from `https://catfact.ninja/fact`; if the external API fails, a fallback value is returned.
- HTTPS redirection is enabled; redirection occurs only when an HTTPS port is configured. In manual Docker runs, stick to HTTP unless you’ve set up certs.


