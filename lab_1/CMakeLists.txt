cmake_minimum_required(VERSION 3.6)
project(lab_1)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=gnu++11")

set(SOURCE_FILES main.cpp)
add_executable(lab_1 main.cpp)