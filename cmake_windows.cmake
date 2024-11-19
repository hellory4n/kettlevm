# windows-toolchain.cmake

set(CMAKE_SYSTEM_NAME Windows)
# fun fact windows 11 still has the nt version of 10
set(CMAKE_SYSTEM_VERSION 10)

# Specify the compiler
set(CMAKE_C_COMPILER x86_64-w64-mingw32-gcc)
set(CMAKE_CXX_COMPILER x86_64-w64-mingw32-g++)

# Ensure CMake looks in the right directories
set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)
set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ONLY)
set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ONLY)
