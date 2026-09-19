#  Mara — Assistant personnel local

> **Mara est un projet personnel d'assistant informatique local inspiré de l'ordinateur et des assistants technologiques de Peter Parker dans l'univers Spider-Man.**
>
> L'objectif est de créer progressivement un assistant capable de comprendre des commandes vocales, de répondre à l'utilisateur et d'interagir directement avec son ordinateur.

---

##  Présentation

**Mara** est un assistant personnel développé en **C# avec Windows Forms**.

Le projet utilise plusieurs technologies afin de transformer une commande vocale en action informatique.

L'idée est de construire Mara progressivement, fonctionnalité par fonctionnalité, tout en comprenant le fonctionnement de chaque composant.

### Exemple

L'utilisateur peut dire :

> « Mara, combien ai-je de RAM ? »

Le fonctionnement est alors :

```text
  Microphone
      ↓
   NAudio
      ↓
Enregistrement audio
      ↓
 Whisper.net
      ↓
 Transcription
      ↓
Ollama / Mara
      ↓
Analyse de la commande
      ↓
 JSON d'action
      ↓
 ActionService
      ↓
    Windows
      ↓
  Résultat
```

---

# 🎯 Objectifs

Les principaux objectifs du projet sont :

* Créer un assistant vocal personnel.
* Utiliser une intelligence artificielle locale.
* Comprendre les modèles de langage.
* Transformer une phrase naturelle en action informatique.
* Interagir avec Windows depuis C#.
* Utiliser des API externes.
* Expérimenter avec la reconnaissance vocale.
* Expérimenter avec la synthèse vocale.
* Construire une architecture logicielle modulaire.
* Améliorer mes compétences en C# et en développement logiciel.

Le projet est développé **progressivement**, afin de comprendre chaque technologie utilisée.

---

#  Architecture

```text
                         ┌──────────────┐
                         │ 🎤 Microphone│
                         └──────┬───────┘
                                ↓
                         ┌──────────────┐
                         │    NAudio    │
                         └──────┬───────┘
                                ↓
                         ┌──────────────┐
                         │ Whisper.net  │
                         └──────┬───────┘
                                ↓
                         ┌──────────────┐
                         │  Ollama / AI │
                         │    Mara      │
                         └──────┬───────┘
                                ↓
                         ┌──────────────┐
                         │     JSON     │
                         └──────┬───────┘
                                ↓
                         ┌──────────────┐
                         │ ActionService│
                         └──────┬───────┘
                                ↓
                    ┌───────────┴───────────┐
                    ↓                       ↓
               🖥️ Windows              🌐 Internet
```

Pour les réponses vocales :

```text
Réponse de Mara
      ↓
    Piper
      ↓
   fichier WAV
      ↓
  SoundPlayer
      ↓
      🔊
```

---

#  Technologies utilisées

| Technologie       | Utilisation                           |
| ----------------- | ------------------------------------- |
| **C#**            | Langage principal                     |
| **.NET 10**       | Framework                             |
| **Windows Forms** | Interface graphique                   |
| **NAudio**        | Capture du microphone                 |
| **Whisper.net**   | Reconnaissance vocale                 |
| **Ollama**        | Exécution de l'IA en local            |
| **Qwen3 1.7B**    | Modèle de langage                     |
| **Piper**         | Synthèse vocale                       |
| **Pexels API**    | Recherche d'images                    |
| **WMI**           | Informations matérielles Windows      |
| **DotNetEnv**     | Gestion des variables d'environnement |
| **Git / GitHub**  | Gestion du code source                |

---

#  Reconnaissance vocale

Mara utilise **NAudio** pour enregistrer la voix depuis le microphone.

La configuration actuelle est :

```text
Fréquence : 16 000 Hz
Canaux    : Mono
Format    : WAV
```

L'enregistrement est sauvegardé dans :

```text
recording.wav
```

Le fichier est ensuite transmis à **Whisper.net**.

---

#  Whisper.net

Whisper transforme l'enregistrement vocal en texte.

Le modèle utilisé actuellement est :

```text
ggml-base.bin
```

La transcription est configurée en français :

```csharp
.WithLanguage("fr")
```

Exemple :

```text
« Combien ai-je de RAM ? »

              ↓

« Combien ai-je de RAM ? »
```

---

# 🧠 Intelligence artificielle locale

Mara utilise **Ollama** afin d'exécuter le modèle d'intelligence artificielle directement sur l'ordinateur.

Le modèle utilisé est basé sur :

```text
Qwen3 1.7B
```

Un modèle personnalisé est également créé :

```text
mara:latest
```

Le modèle est défini grâce au fichier :

```text
Modelfile
```

Il permet notamment d'expliquer à Mara quelles actions elle peut utiliser et sous quel format elle doit les retourner.

---

#  Système d'actions

Une partie importante de Mara est le système d'actions.

L'IA ne contrôle pas directement Windows.

Elle retourne plutôt un **JSON structuré**.

Par exemple :

```json
{
    "action": "system_info",
    "type": "ram"
}
```

C# analyse ensuite ce JSON avec `ActionService`.

Le fonctionnement est donc :

```text
 IA
  ↓
JSON
  ↓
C#
  ↓
Action autorisée
  ↓
Windows
```

Cette séparation permet de garder le contrôle de l'exécution côté application C#.

---

#  Actions disponibles

##  `open_url`

Permet d'ouvrir une adresse Internet.

```json
{
    "action": "open_url",
    "url": "https://www.google.com"
}
```

---

##  `search_web`

Permet de lancer une recherche Web.

```json
{
    "action": "search_web",
    "query": "actualité informatique"
}
```

---

##  `search_image`

Permet de rechercher une image avec l'API Pexels.

```json
{
    "action": "search_image",
    "query": "Paris"
}
```

Le fonctionnement est :

```text
Mara
 ↓
ImageSearchService
 ↓
Pexels API
 ↓
URL de l'image
 ↓
imageVisualisation
 ↓
PictureBox
```

---

##  `open_program`

Permet de lancer certains programmes installés sur l'ordinateur.

Programmes actuellement configurés :

```text
Visual Studio Code
Discord
LibreOffice Writer
LibreOffice Calc
LibreOffice Math
Microsoft Edge
Visual Studio
```

Exemple :

```json
{
    "action": "open_program",
    "program": "vscode"
}
```

---

#  Informations système

Mara peut récupérer différentes informations concernant l'ordinateur grâce à l'action :

```json
{
    "action": "system_info",
    "type": "..."
}
```

Types disponibles :

```text
ram
cpu
disk
gpu
```

---

##  RAM

Mara utilise `ComputerInfo` pour récupérer les informations concernant la mémoire vive.

Elle peut afficher :

```text
RAM totale      : XX.X Go
RAM disponible  : XX.X Go
```

---

##  CPU

Le processeur est récupéré avec **WMI**.

Requête utilisée :

```text
SELECT Name FROM Win32_Processor
```

La classe Windows utilisée est :

```text
Win32_Processor
```

La propriété `Name` permet ensuite de récupérer le nom du processeur.

---

##  Disque

L'espace disque est récupéré avec `DriveInfo`.

Le disque actuellement utilisé est :

```text
C:
```

Mara peut afficher :

```text
Disque C:

Espace total : XX.X Go
Espace libre : XX.X Go
```

---

##  GPU

La carte graphique est récupérée avec WMI.

Classe utilisée :

```text
Win32_VideoController
```

Propriété utilisée :

```text
Name
```

Exemple d'action :

```json
{
    "action": "system_info",
    "type": "gpu"
}
```

---

#  Synthèse vocale

Mara utilise **Piper** pour transformer une réponse textuelle en voix.

```text
Réponse de Mara
      ↓
     Piper
      ↓
   mara.wav
      ↓
 SoundPlayer
      ↓
      🔊
```

La voix française actuellement utilisée est :

```text
fr_FR-siwis-medium.onnx
```

---

#  Variables d'environnement

Les informations sensibles ne doivent pas être directement écrites dans le code.

Par exemple, la clé API Pexels est stockée dans :

```text
.env
```

Exemple :

```env
PEXELS_API_KEY=ma_cle_api
```

C# récupère ensuite la variable :

```csharp
Environment.GetEnvironmentVariable("PEXELS_API_KEY")
```

Le package **DotNetEnv** permet de charger le fichier `.env`.

---

# 🚫 Sécurité Git

Le fichier `.env` contient des informations sensibles et ne doit **pas** être envoyé sur GitHub.

Le fichier `.gitignore` contient notamment :

```gitignore
.env
.vs/
bin/
obj/
*.user
*.suo
*.log
```

Un fichier `.env.example` peut être utilisé pour indiquer les variables nécessaires sans exposer les valeurs.

Exemple :

```env
PEXELS_API_KEY=
```


---

#  Structure du projet

```text
Mara/
│
├── Services/
│   ├── SpeechService.cs
│   ├── OllamaService.cs
│   ├── TtsService.cs
│   ├── ActionService.cs
│   └── ImageSearchService.cs
│
├── Form1.cs
├── Form1.Designer.cs
│
├── imageVisualisation.cs
├── imageVisualisation.Designer.cs
│
├── Program.cs
├── Mara.csproj
│
├── Modelfile
│
├── .env.example
├── .gitignore
│
└── README.md
```

Le fichier `.env` est présent uniquement en local et n'est pas versionné.

---

#  Installation

## Prérequis

Pour utiliser le projet, il faut notamment :

* Windows
* .NET 10
* Visual Studio
* Ollama
* un microphone
* Whisper.net
* un modèle Whisper
* Piper
* une voix Piper

---

##  Packages NuGet

Les principaux packages utilisés sont :

```text
NAudio
Whisper.net
Whisper.net.Ggml
Whisper.net.Runtime
System.Management
DotNetEnv
```

---

#  Installation du modèle Mara

Le modèle personnalisé est créé à partir du fichier :

```text
Modelfile
```

Commande :

```powershell
ollama create mara -f Modelfile
```

Pour lancer Mara :

```powershell
ollama run mara
```

Pour afficher les modèles installés :

```powershell
ollama list
```

---

# Fonctionnement de l'application

Lorsque l'utilisateur appuie sur le bouton :

### 1.  Enregistrement

NAudio démarre le microphone.

```text
Microphone
    ↓
recording.wav
```

### 2.  Transcription

Whisper transforme l'audio en texte.

```text
recording.wav
    ↓
Whisper
    ↓
texte
```

### 3.  Analyse

Le texte est envoyé à Ollama.

```text
texte
  ↓
Ollama
  ↓
JSON ou réponse textuelle
```

### 4. Action

Si l'IA retourne une action :

```text
JSON
 ↓
ActionService
 ↓
Windows
```

### 5. 🔊 Réponse

Si l'IA retourne une réponse normale :

```text
Réponse
  ↓
WinForms
  +
Piper
  ↓
🔊 Mara
```

---

# Évolution du projet

## Fonctionnalités terminées

* [x] Interface Windows Forms
* [x] Capture du microphone
* [x] Reconnaissance vocale avec Whisper
* [x] IA locale avec Ollama
* [x] Modèle personnalisé Mara
* [x] Communication C# → Ollama
* [x] Système d'actions JSON
* [x] Ouverture d'URLs
* [x] Recherche Web
* [x] Recherche d'images
* [x] Affichage d'images
* [x] Ouverture de programmes
* [x] Informations RAM
* [x] Informations CPU
* [x] Informations disque
* [x] Informations GPU
* [x] Synthèse vocale avec Piper
* [x] Gestion des clés API avec `.env`
* [x] Protection du `.env` avec `.gitignore`

## 🔮 Fonctionnalités envisagées

* [ ] Historique des conversations
* [ ] Mémoire de Mara
* [ ] Détection automatique du silence
* [ ] Mot d'activation « Mara »
* [ ] Contrôle avancé de Windows
* [ ] Automatisation de tâches
* [ ] Gestion des fichiers
* [ ] Connexion à un backend Spring Boot
* [ ] Meilleure gestion du contexte
* [ ] Interface graphique plus avancée
* [ ] Personnalisation de la personnalité de Mara
* [ ] Système de plugins/actions extensible

---

# L'objectif principale

Mara est également un projet d'apprentissage.

Son développement permet de travailler différentes notions :

* C#
* .NET
* Programmation orientée objet
* Programmation asynchrone
* JSON
* API REST
* HTTP
* Windows
* WMI
* Gestion des processus
* Audio
* Reconnaissance vocale
* Intelligence artificielle locale
* Modèles de langage
* Synthèse vocale
* Variables d'environnement
* Git et GitHub
* Architecture logicielle

L'objectif est de comprendre **comment chaque technologie fonctionne et comment les assembler pour créer une application complète**.

---

# 🕷️ Inspiration

Le projet Mara est inspiré de l'idée d'un assistant informatique personnel tel qu'on peut en voir dans l'univers de **Spider-Man**, notamment autour de Peter Parker.

Dans cet univers, Peter utilise différentes technologies capables de l'aider dans ses recherches, d'analyser des informations et d'interagir avec différents systèmes.

Mara reprend cette idée dans un contexte réel :

```text
Assistant fictif
      ↓
   Inspiration
      ↓
Technologies réelles
      ↓
      Mara
```

L'objectif n'est pas de reproduire une technologie fictive, mais de construire progressivement **ma propre version d'un assistant personnel intelligent**, avec des technologies réellement disponibles.

---

