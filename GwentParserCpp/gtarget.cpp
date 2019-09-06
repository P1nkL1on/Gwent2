#include "gtarget.h"

GParse *GTarget::createNew() const
{
    return new GTarget();
}

QString GTarget::parseFrom(GAbilityStream &stream)
{
    int index;
    const QString errorMessage = parseEnum(stream, m_targets, "target", index);
    if (errorMessage.isEmpty())
        m_target = Target(index);
    return errorMessage;
}

QString GTarget::toString() const
{
    return toStringEnum(m_targets, "target", m_target);
}
