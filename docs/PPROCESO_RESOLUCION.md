<br>

<p align="center">
  <img src="docs/resources/logo-blanco.png" width="400" alt="ZAP Logo">
</p>
<br>

<h1 align="center">Informe de Proceso de Resolución</h1>

<br>

---

## 1. Interpretación del Desafío
Se interpretó el desafío como la necesidad de una herramienta de **Auditoría Técnica Pre-BIM**. El objetivo central no fue solo el cálculo geométrico, sino asegurar que los datos de entrada sean confiables. El proceso se estructuró en tres fases críticas: 
1. Limpieza y normalización de la ingesta (JSON). 
2. Validación estricta de integridad (Reglas técnicas).
3. Análisis de relaciones espaciales mediante lógica de colisiones.

## 2. Estructuración de la Solución
Se implementó el patrón **MVVM (Model-View-ViewModel)** para garantizar una separación de responsabilidades alineada con los principios **SOLID**:
- **Models:** Entidades de datos puras con lógica geométrica AABB encapsulada.
- **Services:** Motores asíncronos encargados del procesamiento pesado y validación, desacoplados de la UI.
- **ViewModels:** Orquestadores de estado que gestionan la reactividad de la interfaz.
- **Helpers:** Infraestructura de soporte para la visualización y conversión de datos.

## 3. Elección de Tecnologías y Enfoque
- **.NET 10.0 + WPF:** Se seleccionó este stack por representar la vanguardia en desarrollo de escritorio para Windows, ofreciendo el rendimiento necesario para el procesamiento masivo de datos y la capacidad de crear una UI sofisticada.
- **Geometría AABB:** Se utilizó la lógica de *Axis-Aligned Bounding Box* por su alta eficiencia computacional, permitiendo comparaciones espaciales rápidas en grandes volúmenes de objetos.

## 4. Decisiones de Diseño Clave
- **Control de Pares Únicos:** El análisis de intersecciones utiliza estructuras `HashSet<(int, int)>`. Esto garantiza que cada interferencia se registre una sola vez, proporcionando métricas de auditoría precisas y sin redundancias.
- **UX de Diagnóstico:** En lugar de un reporte de texto simple, se desarrolló un **Inspector de Código** dinámico. Esta funcionalidad permite al usuario auditar el JSON original con resaltado visual, emulando la experiencia de herramientas profesionales de la industria.

## 5. Simplificaciones y Sacrificios
Debido al límite de tiempo establecido para la prueba, se tomaron decisiones estratégicas para priorizar la calidad del núcleo del software:
- **Lógica sobre Estética Visual:** Se priorizó el desarrollo de un motor lógico robusto y una UX de diagnóstico por sobre la representación gráfica 3D interactiva. Se consideró que, en una auditoría técnica de modelos, la **integridad y precisión del dato** es prioritaria frente a una visualización básica que no aporta valor analítico inmediato.
- **Persistencia Ágil:** Se optó por la exportación de reportes en formato `.txt` en lugar de la implementación de una base de datos local. Esta decisión favorece la **portabilidad y agilidad**.

## 6. Casos Adicionales Incorporados
Se diseñaron escenarios un poco mas complejos en el archivo `datos_prueba_extensa.json` para validar la robustez del sistema:
- **Contención en Cascada:** Validación de jerarquías complejas (Objeto > Habitación > Edificio).
- **Precisión de Límite:** Escenarios donde los prismas se tocan exactamente en sus caras pero no comparten volumen (deben ser detectados como aislados para evitar falsos positivos de colisión).
