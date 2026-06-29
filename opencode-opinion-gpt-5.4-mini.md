# OpenCode Opinion — gpt-5.4-mini

**Fecha:** 2026-06-12

## Dictamen corto
La rama `origin/zoe` es técnicamente superior como base final.

## Por qué
- Separa responsabilidades de forma correcta.
- Mejora la experiencia de uso.
- Escala mejor que la versión local.

## Lo mejor de `main`
- Más simple.
- Más directa para explicar el ejercicio.
- Menos código, menos abstracción.

## Lo mejor de `origin/zoe`
- `Program`, `Automata`, `Estado` y `CatalogoAutomatas` están bien separados.
- Menú en loop con validación de entrada.
- Lenguajes y autómatas descritos con claridad.

## Lo peor de `main`
- Mucho acoplamiento.
- Poca validación.
- Difícil de extender sin ensuciar `Program`.

## Lo peor de `origin/zoe`
- Más boilerplate.
- Algo más pesada para un TP chico.

## Conclusión
Si el objetivo es tener una base mantenible y prolija, elegiría `origin/zoe`. Si el objetivo es solo mostrar la idea más rápido, `main` alcanza, pero arquitectónicamente queda atrás.
