# TelemetryApi

A robust, secure REST API for managing industrial telemetry infrastructure and sensor data collection. Built for environmental monitoring, IoT applications, and regulatory compliance with State Pollution Control Board (SPCB) requirements.

## 🚀 Features

### CRUD Operations
Complete create, read, update, and delete operations for:
- **Industry (Company)** - Facility management with regulatory compliance fields
- **Station** - Monitoring locations, emission stacks, and measurement points  
- **Sensor (Channel)** - Individual pollutant parameters and analyzers

### Secure Data Ingestion
- **Live Data Upload** - Real-time sensor data transmission
- **Delayed Data Upload** - Store-and-forward capabilities for network disruptions
- **AES Encryption** - Application-layer encryption prevents data tampering during upload
- **Public Key Distribution** - End users receive public keys for data encryption
- **Server-side Decryption** - Private key decryption ensures secure data processing

## 🏗️ Architecture

```
TelemetryApi (Controllers)
    ↓
Telemetry.Business (Services)
    ↓
Telemetry.Repositories (Data Access)
    ↓
PostgreSQL (Metadata) + Redis (Time-Series Data)
```

## 🔐 Security Features

- **AES Encryption Algorithm** - All sensor data uploads use AES encryption for maximum security
- **Public Key Distribution** - End users receive public keys to encrypt their sensor data
- **Private Key Decryption** - Server uses private keys for secure data decryption
- **Tamper Protection** - Cryptographic validation prevents data modification during transit
- **Authentication** - JWT-based access control for all endpoints
- **Replay Protection** - Prevents duplicate or malicious data submissions

## 📡 API Endpoints

### Industry Management
```http
POST   /api/v1/industries           # Create industry
GET    /api/v1/industries           # List industries  
GET    /api/v1/industries/{id}      # Get industry details
PUT    /api/v1/industries/{id}      # Update industry
DELETE /api/v1/industries/{id}      # Delete industry
```

### Station Management
```http
POST   /api/v1/industries/{id}/stations         # Create station
GET    /api/v1/industries/{id}/stations         # List stations
GET    /api/v1/stations/{id}                    # Get station details
PUT    /api/v1/stations/{id}                    # Update station
DELETE /api/v1/stations/{id}                    # Delete station
```

### Sensor (Channel) Management
```http
POST   /api/v1/stations/{id}/sensors            # Create sensor
GET    /api/v1/stations/{id}/sensors            # List sensors
GET    /api/v1/sensors/{id}                     # Get sensor details
PUT    /api/v1/sensors/{id}                     # Update sensor
DELETE /api/v1/sensors/{id}                     # Delete sensor
```

### Data Upload Endpoints
```http
POST   /api/v1/sensor-data                      # Upload encrypted sensor data (live/delayed)
GET    /api/v1/sensor-data                      # Query historical data
GET    /api/v1/encryption/public-key            # Get public key for data encryption
```

## 📊 Data Structure

### Industry Fields
- Company Name, Industry Type/Category
- Registered Address, State & District
- Consent/Authorization Numbers
- Latitude & Longitude, Contact Details
- Production Capacity, Pollution Control Devices

### Station Fields
- Station Name/ID, Location Type
- GPS Coordinates, Height/Depth
- Associated Industry, Installation Date
- Pollutant Source Type, Channel Count

### Sensor Fields
- Parameter Name (PM2.5, PM10, SO2, NOx, pH, etc.)
- Unit of Measurement, Analyzer Make/Model
- Calibration Date, Detection Range
- Data Interval, Communication Protocol

## 🔧 Technology Stack

- **Framework**: ASP.NET Core Web API
- **Database**: PostgreSQL (metadata) + Redis (time-series)
- **Authentication**: JWT tokens
- **Encryption**: AES algorithm with public/private key pair
- **ORM**: Entity Framework Core

## 🚦 Getting Started

1. **Authentication**
   ```bash
   POST /api/v1/auth/login
   ```

2. **Get Public Key for Encryption**
   ```bash
   GET /api/v1/encryption/public-key
   Authorization: Bearer <token>
   ```

3. **Create Industry**
   ```bash
   POST /api/v1/industries
   Authorization: Bearer <token>
   ```

4. **Add Monitoring Station**
   ```bash
   POST /api/v1/industries/{id}/stations
   Authorization: Bearer <token>
   ```

5. **Configure Sensors**
   ```bash
   POST /api/v1/stations/{id}/sensors
   Authorization: Bearer <token>
   ```

6. **Upload Encrypted Sensor Data**
   ```bash
   POST /api/v1/sensor-data
   Authorization: Bearer <token>
   Content-Type: application/json
   
   {
     "company_id": "123",
     "station_id": "456",
     "channel_id": "789",
     "encrypted_data": "AES_ENCRYPTED_PAYLOAD_HERE",
     "timestamp": "2025-07-18T01:02:00Z",
     "signature": "HMAC_SIGNATURE"
   }
   ```

## 🛡️ Data Security & Encryption

### How It Works:
1. **Public Key Distribution**: End users receive public keys through the API
2. **Client-side Encryption**: Users encrypt sensor data using provided public keys and AES algorithm
3. **Secure Transmission**: Encrypted data is sent to the server via HTTPS
4. **Server Decryption**: Server uses private keys to decrypt and process the data
5. **Data Integrity**: HMAC signatures ensure data hasn't been tampered with

### Security Benefits:
- **End-to-End Protection**: Data is encrypted from sensor to server
- **Tamper-Proof**: Any modification during transmission is detected
- **User-Friendly**: Public keys enable easy integration with existing sensor software
- **Regulatory Compliance**: Meets data security requirements for industrial monitoring

## 📱 Client Integration

End users can integrate with their existing sensor software by:
1. Obtaining public keys from the API
2. Implementing AES encryption in their sensor applications
3. Sending encrypted payloads to the data upload endpoints
4. Ensuring data integrity with proper signature validation

## 🎯 Use Cases

- **Environmental Compliance** - SPCB/CPCB regulatory monitoring
- **Industrial IoT** - Real-time encrypted sensor data collection
- **Air Quality Monitoring** - Secure PM2.5, PM10, SO2, NOx tracking
- **Water Quality Monitoring** - Protected pH, BOD, COD, TSS measurement
- **Emission Monitoring** - Tamper-proof stack emission compliance

## 📋 Compliance

Designed to meet Indian pollution control standards:
- Central Pollution Control Board (CPCB) guidelines
- State Pollution Control Board (SPCB) requirements
- Online Continuous Emission Monitoring System (OCEMS) standards
- National Ambient Air Quality Standards (NAAQS)

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🤝 Contributing

Contributions are welcome! Please read the contributing guidelines before submitting pull requests.

---

**TelemetryApi** - Secure, scalable, and compliant industrial telemetry management with AES encryption and public key distribution for tamper-proof data uploads.
