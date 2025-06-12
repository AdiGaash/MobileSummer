# Project Guidelines

## General Code Guidelines

1. **Script Placement**
    - All scripts MUST be placed in the `_Scripts` folder
    - Editor scripts MUST be placed in `_Scripts/Editor` folder
    - Do NOT create scripts in any other location

2. **Naming Conventions**
    - Use PascalCase for class names (e.g., `PlayerController`)
    - Use camelCase for variables (e.g., `playerHealth`)
    - Use ALL_CAPS for constants (e.g., `MAX_HEALTH`)
    - Script filename must match the class name exactly

3. **Namespace Usage**
    - Do NOT use namespaces in any scripts
    - All classes should be in global scope
    - Avoid using 'using' directives for custom namespaces

4. **Code Structure**
    - One class per file
    - Organize public variables at the top of the class
    - Group similar functions together
    - Comment complex logic
    - Keep methods short and focused on single responsibility

## AI Script Creation Guidelines

### Instructions for AI When Creating New Scripts

1. **Script Location**
    - ALWAYS place new scripts in the `_Scripts` folder
    - For editor scripts, use `_Scripts/Editor` folder

2. **Namespace Requirements**
    - NEVER create new namespaces
    - Do NOT wrap code in namespace blocks
    - Remove any auto-generated namespace declarations

3. **Class Structure**
    - Create one class per file
    - Match filename exactly to class name
    - Use appropriate inheritance (MonoBehaviour, ScriptableObject, etc.)

4. **Code Style**
    - Include XML documentation for public methods
    - Use [SerializeField] for inspector-visible private fields
    - Initialize collections in Awake() or Start() methods
    - Follow Unity's execution order conventions

### Example Formats for AI

#### Standard MonoBehaviour
using System;

/// <summary>
/// ${DESCRIPTION}
/// </summary>
public class ${NAME} 
{
    ${END}
}