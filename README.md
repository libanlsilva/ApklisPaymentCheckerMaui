# ApklisPaymentCheckerMaui

**Verificador de Pagos para Apklis** - Una aplicación .NET MAUI para verificar el estado de pago de aplicaciones en la tienda cubana [Apklis](https://apklis.cu/).

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-blue.svg)](https://dotnet.microsoft.com/en-us/apps/maui)
[![Android](https://img.shields.io/badge/Android-21%2B-green.svg)](https://developer.android.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)

## 📱 Descripción

Esta aplicación permite a los desarrolladores verificar si sus aplicaciones han sido compradas por usuarios en la tienda oficial de aplicaciones de Cuba, **Apklis**. La aplicación utiliza el Content Provider de Apklis para consultar el estado de pago y obtener información del usuario que realizó la compra.

## ✨ Características

- 🔍 **Verificación de pagos**: Consulta el estado de pago de cualquier aplicación en Apklis
- 👤 **Información de usuario**: Obtiene el nombre del usuario que realizó el pago
- 📱 **Interfaz moderna**: UI responsive desarrollada con .NET MAUI
- 🛡️ **Manejo de errores**: Gestión robusta de excepciones y validaciones
- 🎯 **Fácil de usar**: Interfaz intuitiva con validación de entrada

## 🏗️ Arquitectura

El proyecto utiliza una arquitectura limpia con separación de responsabilidades:

```
📁 ApklisPaymentCheckerMaui/
├── 📁 Interfaces/
│   └── IApklisPaymentChecker.cs          # Interfaz del verificador
├── 📁 Platforms/
│   └── 📁 Android/
│       ├── ApklisPaymentChecker.cs       # Implementación Android
│       ├── AndroidManifest.xml           # Permisos y configuración
│       └── MainActivity.cs               # Actividad principal
├── MainPage.xaml                         # UI principal
├── MainPage.xaml.cs                      # Lógica de la UI
├── MauiProgram.cs                        # Configuración y DI
└── ApklisPaymentCheckerMaui.csproj      # Configuración del proyecto
```

## 🚀 Requisitos

### Desarrollo
- **Visual Studio 2022** (v17.8 o superior) con cargas de trabajo de .NET MAUI
- **.NET 9.0** SDK
- **Android SDK** (API nivel 21 o superior)

### Dispositivo de destino
- **Android 5.0** (API nivel 21) o superior
- **Apklis** instalado en el dispositivo
- La aplicación a verificar debe estar instalada desde Apklis

## 📦 Instalación y Configuración

### 1. Clonar el repositorio
```bash
git clone https://github.com/libanlsilva/ApklisPaymentCheckerMaui.git
cd ApklisPaymentCheckerMaui
```

### 2. Abrir en Visual Studio
- Abrir `ApklisPaymentCheckerMaui.sln` en Visual Studio 2022
- Restaurar paquetes NuGet automáticamente

### 3. Configurar Android
El proyecto ya incluye los permisos necesarios en `AndroidManifest.xml`:
```xml
<queries>
  <package android:name="cu.uci.android.apklis" />
  <provider android:authorities="cu.uci.android.apklis.PaymentProvider" />
</queries>
```

### 4. Compilar y ejecutar
- Seleccionar un dispositivo Android o emulador
- Presionar `F5` para compilar y ejecutar

## 🎯 Uso

1. **Ejecutar la aplicación** en un dispositivo Android con Apklis instalado
2. **Introducir el nombre del paquete** de la aplicación a verificar
   - Formato: `com.empresa.nombreapp`
   - Ejemplo: `com.yourcompany.appname`
3. **Presionar "Verificar Pago"** para consultar el estado
4. **Ver los resultados**:
   - ✅ **Aplicación pagada** - Muestra el estado y nombre del usuario
   - ❌ **Aplicación no pagada** - Indica que no se ha realizado el pago

## 🔧 Componentes Técnicos

### ApklisPaymentChecker
La clase principal que interactúa con el Content Provider de Apklis:

```csharp
public (bool IsPaid, string UserName) GetPaymentInfo(string packageName)
```

**Funcionalidades:**
- Consulta el Content Provider: `content://cu.uci.android.apklis.PaymentProvider/app/`
- Obtiene información de pago y usuario
- Maneja errores y excepciones de forma segura

### Interfaz IApklisPaymentChecker
Define el contrato para la verificación de pagos:
```csharp
public interface IApklisPaymentChecker
{
    (bool IsPaid, string UserName) GetPaymentInfo(string packageName);
}
```

### Dependency Injection
Configurado en `MauiProgram.cs` para inyección de dependencias:
```csharp
#if ANDROID
builder.Services.AddSingleton<IApklisPaymentChecker, Platforms.Android.ApklisPaymentChecker>();
#endif
```

## 🛠️ Personalización

### Modificar la interfaz
Editar `MainPage.xaml` y `MainPage.xaml.cs` para personalizar:
- Colores y estilos
- Campos adicionales
- Validaciones personalizadas

### Extender funcionalidades
- Agregar soporte para múltiples tiendas
- Implementar caché de resultados
- Añadir logs detallados

## 📋 Limitaciones

- ⚠️ **Solo Android**: Funciona únicamente en dispositivos Android
- 🏪 **Requiere Apklis**: Debe estar instalado en el dispositivo
- 📱 **Apps de Apklis**: Solo verifica aplicaciones instaladas desde Apklis
- 🔒 **Permisos**: Requiere acceso al Content Provider de Apklis

## 🤝 Contribución

Las contribuciones son bienvenidas. Para contribuir:

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -am 'Añadir nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Crear un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE.txt](LICENSE.txt) para más detalles.

## 🌐 Enlaces Útiles

- [Apklis - Tienda oficial de aplicaciones de Cuba](https://apklis.cu/)
- [Documentación .NET MAUI](https://docs.microsoft.com/en-us/dotnet/maui/)
- [Guía de desarrollo Android](https://developer.android.com/guide)

## 👨‍💻 Autor

Desarrollado como ejemplo para la integración con la tienda de aplicaciones Apklis de Cuba.

---
*Este es un proyecto de ejemplo para demostrar la verificación de pagos en Apklis. No es un producto oficial de Apklis.*
