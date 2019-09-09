#ifndef GERRORHANDLER_H
#define GERRORHANDLER_H

#include "gparseerr.h"
#include "gabilitystream.h"

namespace GErrorHandler {
    bool show(const GErr &operationRes, const GAbilityStream &stream);
}

#endif // GERRORHANDLER_H
