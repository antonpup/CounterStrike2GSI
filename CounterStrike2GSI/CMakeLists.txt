# Setup project.
SET(PRODUCT CounterStrike2GSI)
SET(PRODUCT_NAME "${PRODUCT}")
SET(PRODUCT_VERSION 1.0.2)
SET(PRODUCT_DESCRIPTION "A C# library to interface with the Game State Integration found in Counter-Strike 2.")
SET(PRODUCT_AUTHORS "Anton Pupkov")
SET(PRODUCT_COPYRIGHT "Copyright© 2024")

if(DEFINED BUILD_NUMBER)
    SET(PRODUCT_VERSION "${PRODUCT_VERSION}.${BUILD_NUMBER}")
endif()

PROJECT(${PRODUCT} VERSION ${PRODUCT_VERSION} LANGUAGES CSharp)

# Setup project to use C# utilities.
INCLUDE(CSharpUtilities)

SET(SOURCES
    CS2EventsInterface.cs
    CS2GSIFile.cs
    EventDispatcher.cs
    EventHandler.cs
    EventsInterface.cs
    GameState.cs
    GameStateListener.cs
    GameStateHandler.cs
    EventMessages/AllGrenadesEvents.cs
    EventMessages/AllPlayerEvents.cs
    EventMessages/AuthEvents.cs
    EventMessages/BaseEvent.cs
    EventMessages/BombEvents.cs
    EventMessages/CS2GameEvent.cs
    EventMessages/KillfeedEvents.cs
    EventMessages/MapEvents.cs
    EventMessages/PhaseCountdownEvents.cs
    EventMessages/PlayerEvents.cs
    EventMessages/ProviderEvents.cs
    EventMessages/RoundEvents.cs
    EventMessages/UpdateEvent.cs
    Nodes/Bomb.cs
    Nodes/AllGrenades.cs
    Nodes/Auth.cs
    Nodes/AllGrenadesProvider/Grenade.cs
    Nodes/AllPlayers.cs
    Nodes/PhaseCountdowns.cs
    Nodes/Round.cs
    Nodes/Player.cs
    Nodes/PlayerProvider/MatchStats.cs
    Nodes/PlayerProvider/PlayerState.cs
    Nodes/PlayerProvider/Weapon.cs
    Nodes/Map.cs
    Nodes/MapProvider/TeamStatistics.cs
    Nodes/Provider.cs
    Nodes/TournamentDraft.cs
    Nodes/Node.cs
    Nodes/Helpers/Vector2D.cs
    Nodes/Helpers/Vector3D.cs
    StateHandlers/AllGrenadesHandler.cs
    StateHandlers/AllPlayersHandler.cs
    StateHandlers/AuthHandler.cs
    StateHandlers/BombHandler.cs
    StateHandlers/KillfeedHandler.cs
    StateHandlers/MapHandler.cs
    StateHandlers/PhaseCountdownHandler.cs
    StateHandlers/PlayerHandler.cs
    StateHandlers/ProviderHandler.cs
    StateHandlers/RoundHandler.cs
    Utils/SteamUtils.cs
)

SET(README "${CMAKE_SOURCE_DIR}/README.md")

# Add shared library project.
ADD_LIBRARY(${PROJECT_NAME} SHARED
    ${README}
    ${SOURCES}
)

# Define dependencies.
TARGET_LINK_LIBRARIES(${PROJECT_NAME} 
    PUBLIC CommonLib
)

SET(NET_REFERENCES
    )

SET(NUGET_PACKAGES
    "Microsoft.CSharp_4.7.0"
    "System.Data.DataSetExtensions_4.5.0"
    "Newtonsoft.Json_13.0.3"
    )

# Set .Net
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    DOTNET_SDK "Microsoft.NET.Sdk"
    DOTNET_TARGET_FRAMEWORK "net8.0"
    VS_DOTNET_REFERENCES "${NET_REFERENCES}"
    VS_GLOBAL_ROOTNAMESPACE ${PROJECT_NAME}
    VS_PACKAGE_REFERENCES "${NUGET_PACKAGES}"
)

# Set readme file
SET_SOURCE_FILES_PROPERTIES(${README} PROPERTIES
    VS_CSHARP_Pack "True"
    VS_CSHARP_PackagePath "\\")

# Set project configuration
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    VS_GLOBAL_GenerateAssemblyInfo "True"
    VS_GLOBAL_GenerateDocumentationFile "True"
    VS_GLOBAL_GeneratePackageOnBuild "True"
    VS_GLOBAL_TreatWarningsAsErrors "True"
    VS_GLOBAL_PackageProjectUrl ${REPOSITORY_URL}
    VS_GLOBAL_RepositoryUrl ${REPOSITORY_URL}
    VS_GLOBAL_PackageReadmeFile "README.md"
)

# Set Assembly information
SET_TARGET_PROPERTIES(${PROJECT_NAME} PROPERTIES
    VS_GLOBAL_Authors "${PRODUCT_AUTHORS}"
    VS_GLOBAL_Version "${PRODUCT_VERSION}"
    VS_GLOBAL_Description "${PRODUCT_DESCRIPTION}"
    VS_GLOBAL_Product "${PRODUCT_NAME}"
    VS_GLOBAL_Copyright "${PRODUCT_COPYRIGHT}"
)

# Export config.
EXPORT(TARGETS ${PROJECT_NAME} FILE ${PROJECT_NAME}Config.cmake)