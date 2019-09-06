#ifndef GTAG_H
#define GTAG_H

#include "gnegatable.h"

class GTag : public GNegatable
{
public:
    GTag() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Tag {
        anytag = 0,
        leader = 1,
        doomed,
        cursed,
        beast,
        elf,
        ogroid,
        construct,
        vodyanoi,
        relict,
        wildhunt,
        insectoid,
        vampire,
        necrophage,
        draconid,
        dwarf,
        dryad,
        machine,
        soldier,
        officer,
        support,
        cultist,
        mage,
        witcher,
        clanAnCraite,
        clanTuirseach,
        clanHeyMaey,
        clanDrummond,
        clanDimun,
        clanTordarroch,
        clanBrokvar,
        kaedwen,
        temeria,
        redania,
        aedirn,
        cintra,
        special,
        spell,
        item,
        alchemy,
        tactic,
        organic,
        hazard,
        boon
    };
    const QStringList m_tags = QStringList()
            << QString()
            << "leader"
            << "doomed"
            << "cursed"
            << "beast"
            << "elf"
            << "ogroid"
            << "construct"
            << "vodyanoi"
            << "relict"
            << "wildhunt"
            << "insectoid"
            << "vampire"
            << "necrophage"
            << "draconid"
            << "dwarf"
            << "dryad"
            << "machine"
            << "soldier"
            << "officer"
            << "support"
            << "cultist"
            << "mage"
            << "witcher"
            << "clan an craite"
            << "clan tuirseach"
            << "clan hey maey"
            << "clan drummond"
            << "clan dimun"
            << "clan tordarroch"
            << "clan brokvar"
            << "kaedwen"
            << "temeria"
            << "redania"
            << "aedirn"
            << "cintra"
            << "special"
            << "spell"
            << "item"
            << "alchemy"
            << "tactic"
            << "organic"
            << "hazard"
            << "boon";
    Tag m_tag;
};


#endif // GTAG_H
