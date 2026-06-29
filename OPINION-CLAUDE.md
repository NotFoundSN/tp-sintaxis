# Opinión técnica — Comparación `main` vs `zoe`

> Informe generado por Claude. Comparativa técnica entre la versión local (`main`)
> y la rama del otro dev (`origin/zoe`) del TP de Sintaxis (AFD por consola).

## 0. Aclaración estructural (importa)

`zoe` **no es una rama derivada de `main`**. Son dos historias de git
independientes, sin ancestro común (`git merge-base main origin/zoe` devuelve
vacío). El otro dev reescribió el proyecto de cero y renombró todo:
`tp-sintaxys/` → `SintaxisTPZoe/`. Un merge directo será un choque de archivos,
no un merge limpio.

---

## 1. Lo que es IGUAL en ambas

Ambas implementan un **AFD (autómata finito determinista)** que valida cadenas,
con menú por consola. Los autómatas son casi los mismos:

| Autómata | main | zoe | ¿Idénticos? |
|---|---|---|---|
| A / L3 | `aⁿb` | `aⁿb` | ✅ Sí |
| B / L4 | (ab)... + c | (ab)... + c | ⚠️ **Casi** (ver §6) |
| C / L5 | mismas transiciones | mismas transiciones | ✅ Sí |

La lógica de evaluación comparte la misma idea: estado inicial → consumir
símbolos → ¿terminó en estado final?

---

## 2. Diferencia de fondo: arquitectura

| Aspecto | **main (versión local)** | **zoe** |
|---|---|---|
| Clases | `node`, `transition`, `Program` | `Estado`, `Automata`, `CatalogoAutomatas`, `Program` |
| Guardar transiciones | `List<transition>` + `FirstOrDefault` (O(n)) | `Dictionary<char, Estado>` (O(1)) |
| Separación de responsabilidades | Todo en `Program` (autómatas + evaluación + UI) | Capas: Estado / Automata / Catálogo / UI |
| Agregar un autómata nuevo | 3 lugares (case del switch + método + línea de menú) | 1 lugar (un factory + agregarlo a la lista) |
| Convención C# | `node`, `transition` en minúscula ❌ rompe PascalCase | `Estado`, `Automata` ✅ idiomático |
| C# moderno | No usa nullable refs | `Estado?`, `string?`, `TryParse`, `TryGetValue` |

---

## 3. Lo BUENO de zoe

- **Determinismo garantizado por diseño.** El `Dictionary<char, Estado>` hace
  imposible tener dos transiciones con el mismo símbolo en un estado. Eso ES la
  invariante de un AFD. `main`, con `List`, deja agregar dos transiciones `'a'`
  en el mismo estado y se queda con la primera en silencio. Detalle fino: el
  último commit de `main` se llama *"enforce DFA invariants"*, pero
  estructuralmente **zoe la fuerza mejor**.
- **Separación de responsabilidades real.** `Automata` evalúa, `Estado` modela,
  `CatalogoAutomatas` es la "data", `Program` solo es UI. SRP de manual.
- **Extensibilidad (OCP).** Sumar un autómata = un método + una línea.
- **UX superior.** Loop con re-prompt, `Console.Clear`, colores, valida la
  opción con `int.TryParse`, menú generado dinámico desde la lista. `main` corre
  **una sola vez y termina**, y si tipeás algo que no es 1/2/3 se cierra sin avisar.

## 4. Lo BUENO de main (lo que zoe perdió)

- **Feedback de error granular.** `testWord` distingue *por qué* falló:
  `"Palabra no valida"` (no había transición) vs `"El nodo donde finalizó no es
  un nodo terminal"` (la cadena se consumió pero quedó en estado no final). zoe
  solo dice "SÍ/NO pertenece" — perdió esa distinción didáctica, valiosa en un
  TP de sintaxis.

Esa es la única ventaja técnica genuina de `main`.

---

## 5. Lo MALO de cada uno

**main tiene en contra:**
- Código muerto comentado dentro de `testWord` (el `foreach` viejo).
- `catch (Exception e)` que se traga TODO y devuelve `"Error no contemplado"` —
  esconde bugs.
- Guarda inútil en `addTransition`: `character != null` con un `char` es
  **siempre true** (warning del compilador). No protege nada.
- Búsqueda lineal de transiciones (irrelevante en performance acá, pero
  conceptualmente peor).
- No respeta PascalCase en clases (`node`, `transition`).

**zoe tiene en contra:**
- `EsFinal { get; set; }` — el `set` público deja mutar si un estado es final
  desde afuera. Fuga de encapsulamiento (chica, pero está).
- Perdió el feedback de error granular (§4).
- Comentarios informales ("perdón, era chiste pero tenía que poner rosa"). Para
  un TP da igual, pero no es prolijo.

---

## 6. ⚠️ Divergencia semántica — verificar con la consigna

Diferencia de **comportamiento real** en el autómata B / L4:

- En **main**, el estado `q3` es **final** (`new node("q3", true)`).
- En **zoe**, `q3` **NO es final** (`new Estado("q3")`).

Consecuencia concreta: la cadena **`"abc"`** (y `"abcc"`, etc.) → en **main se
ACEPTA**, en **zoe se RECHAZA**.

Pista fuerte: la **propia descripción de zoe** dice
`{ (ab)ⁿ · (cᵖ | b) | n ≥ 1, p ≥ 0 }`. Con `p ≥ 0`, las `c` tras `ab` deberían
aceptarse → el código de zoe **se contradice con su propia descripción**. Apunta
a que zoe tiene un bug ahí (q3 debería ser final) y `main` está correcto en ese
autómata. No es certeza al 100% — depende de la consigna exacta del TP.

---

## 7. Veredicto

Como **arquitectura**, zoe le pasa el trapo a main: separación de capas,
determinismo por diseño, extensibilidad, UX, C# idiomático. Para seguir
construyendo, **la base de zoe es mejor punto de partida**.

PERO main tiene dos cosas que NO conviene perder: el **feedback de error
granular** y, posiblemente, el **q3 final correcto** en el autómata B.

**Recomendación:** tomar la arquitectura de zoe como base y portar esos dos
aciertos de `main` (mensajes de error distinguidos + verificar el q3 contra la
consigna).
