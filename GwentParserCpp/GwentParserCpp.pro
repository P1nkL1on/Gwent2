QT -= gui

CONFIG += c++11 console
CONFIG -= app_bundle

# The following define makes your compiler emit warnings if you use
# any feature of Qt which as been marked deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if you use deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += main.cpp \
    gabilitystream.cpp \
    gcolor.cpp \
    gcolorcondition.cpp \
    gnegate.cpp \
    gnegatable.cpp \
    gparse.cpp \
    ghost.cpp \
    gunits.cpp \
    gunit.cpp \
    gplace.cpp \
    gtarget.cpp \
    gtag.cpp \
    gtagcondition.cpp \
    gplacecondition.cpp \
    gparseerr.cpp \
    gcondition.cpp

HEADERS += \
    gparse.h \
    gabilitystream.h \
    gcolor.h \
    gcolorcondition.h \
    gnegate.h \
    gnegatable.h \
    ghost.h \
    gunits.h \
    gunit.h \
    gplace.h \
    gtarget.h \
    gtag.h \
    gtagcondition.h \
    gplacecondition.h \
    gparseerr.h \
    gcondition.h
