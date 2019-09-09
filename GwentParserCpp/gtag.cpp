#include "gtag.h"

GParse *GTag::createNew() const
{
    GTag* cop = new GTag();
    cop->m_tag = m_tag;
    return cop;
}

GErr GTag::parseFrom(GAbilityStream &stream)
{
    GAbilityStream streamCopy = stream;
    QString currentTag = streamCopy.nextWord();
    int words = 1;
    do{
        if (streamCopy.end())
            break;
        const int index = m_tags.indexOf(currentTag);
        if (index < 0){
            currentTag += QString(" %1").arg(streamCopy.nextWord());
            ++words;
            continue;
        }
        m_tag = Tag(index);
        stream = streamCopy;
        return GErr();
    }while(words < 3);
    return GErr(stream.pos(), QString("'%1' is not a tag!").arg(currentTag.split(' ').first()));
}

QString GTag::toString() const
{
    return toStringEnum(m_tags, "tag", m_tag);
}
