<br>

<p align="center">
  <img src="resources/logo-blanco.png" width="400" alt="ZAP Logo">
</p>
<br>

<h1 align="center">Informe de Implementación de IA Estratégica</h1>

<br>

---

## 1. Filosofía de Trabajo
Para el desarrollo de esta herramienta, se adoptó un enfoque de **co-creación técnica**, integrando modelos de IA no solo para la generación de código, sino como un consultor de optimización y validación geométrica. Este método permite acelerar el ciclo de desarrollo manteniendo un control estricto sobre la calidad del software.

## 2. Ecosistema de Herramientas
- **Studio IA (Gemini):** Motor de razonamiento para colisiones y arquitectura MVVM.
- **Visual Studio Copilot:** Asistente de tipado y estándares de código en tiempo real.
- **ChatGPT (OpenAI):** Consultor editorial para el refinamiento de la documentación técnica y alineación con estándares de la industria.

## 3. Estrategia de Prompt Engineering y Resultados
A continuación, se describen los prompts representativos que demuestran el uso eficaz de la herramienta para resolver desafíos específicos de la industria:

*   **Optimización Algorítmica:**
    > *"Actúa como un ingeniero de software senior experto en geometría computacional. Necesito optimizar la detección de intersecciones en una colección masiva de prismas AABB. Sugiere una estructura de datos que permita evitar comparaciones redundantes (pares duplicados) y mantenga la complejidad temporal controlada."*
    *   **Resultado:** Implementación de `HashSet<(int, int)>` y el uso de `relatedIds` para identificar objetos aislados en una sola pasada de análisis.

*   **Coherencia de Marca y UX:**
    > *"Analiza la estética de un estudio de arquitectura de alta gama: sobriedad, minimalismo y uso de espacios negativos. Genera un diccionario de recursos XAML para WPF que utilice una paleta basada en #171817 y #E9E9E7, aplicando efectos de iluminación al hover de manera sutil para elementos interactivos."*
    *   **Resultado:** Interfaz sofisticada con feedback visual inmediato en comandos de carga y descarga.

*   **Diagnóstico y Depuración:**
    > *"El motor de serialización de System.Text.Json falla al encontrar comentarios en el archivo fuente. Cuál es la configuración más robusta para ignorar comentarios y permitir comas finales sin comprometer la integridad de la validación técnica?"*
    *   **Resultado:** Configuración avanzada de `JsonSerializerOptions` con `JsonCommentHandling.Skip`.

*   **Redacción Técnica y Storytelling de Negocio (ChatGPT):**
    > "Actúa como un redactor técnico senior especializado en la industria de la construcción. Corrije los documentos de texto que te paso y ayúdame a estructurarlo para así obtener un informe de resolución que resalte el valor estratégico de la auditoría técnica para ZAP Arquitectos. El tono debe ser sofisticado y profesional."
    *   **Resultado:** Documentación técnica de alto impacto que comunica eficazmente las decisiones de ingeniería tomadas (Estas líneas incluidas).

## 4. Supervisión Crítica y Ajustes Manuales
La intervención humana fue fundamental para refinar las sugerencias de la IA que no cumplían con los estándares de producción:
- **Corrección de XAML:** Se descartaron atributos de diseño inexistentes en el estándar de WPF (como `LetterSpacing`), ajustando la UI mediante técnicas manuales de composición.
- **Contexto de Negocio:** Se reformularon los mensajes de error para que hablaran el lenguaje del arquitecto (ej. "Contención anidada", "Aislamiento espacial") en lugar de términos puramente informáticos.
- **Refactorización SOLID:** Se reorganizó la lógica sugerida inicialmente por la IA para asegurar que el `AnalyzerService` no tuviera dependencias con la capa visual, garantizando la testabilidad del sistema.

## 5. Conclusión
El uso de **Studio IA (Gemini)** permitió entregar una herramienta que supera el alcance básico de la prueba, ofreciendo una experiencia de usuario (UX) de nivel industrial y una robustez lógica que garantiza la fiabilidad del análisis espacial.
