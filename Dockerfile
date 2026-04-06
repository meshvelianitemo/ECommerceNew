# ============================================================
#  SEQ - Standalone Development Server
#  For: Clean Architecture eCommerce Application
#  Author: Dev Setup
# ============================================================

FROM datalust/seq:latest

# Accept the EULA automatically
ENV ACCEPT_EULA=Y

# Data directory inside the container
ENV DATA_DIR=/data

# Base URI (empty = root)
ENV BASE_URI=

# Timezone
ENV TZ=Etc/UTC

# SEQ default admin password for dev (plaintext passthrough via env)
# In production, replace with a hashed password
ENV SEQ_FIRSTRUN_ADMINPASSWORD=DevPassword1234!

# Expose ports:
#   5341 = Seq ingestion (where your app sends logs)
#   80   = Seq web UI
EXPOSE 80
EXPOSE 5341

# Data volume so logs persist across container restarts
VOLUME ["/data"]

# Healthcheck — polls the Seq API endpoint
HEALTHCHECK --interval=15s --timeout=5s --start-period=20s --retries=3 \
  CMD wget --quiet --tries=1 --spider http://localhost/api || exit 1
