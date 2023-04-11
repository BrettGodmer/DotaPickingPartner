﻿namespace Dota_Picking_Partner.Models
{
    public class HeroSelections
    {
        public HeroNames heroOne { get; set; }
        public HeroNames heroTwo { get; set; }
        public HeroNames heroThree { get; set; }
        public HeroNames heroFour { get; set; }
        public HeroNames heroFive { get; set; }
    }

    public enum HeroNames
    {
        n_a,
        abaddon,
        alchemist,
        ancient_apparition,
        anti_mage,
        arc_warden,
        axe,
        bane,
        batrider,
        beastmaster,
        bloodseeker,
        bounty_hunter,
        brewmaster,
        bristleback,
        broodmother,
        centaur_warrunner,
        chaos_knight,
        chen,
        clinkz,
        clockwerk,
        crystal_maiden,
        dark_seer,
        dark_willow,
        dazzle,
        death_prophet,
        disruptor,
        doom,
        dragon_knight,
        drow_ranger,
        earth_spirit,
        earthshaker,
        elder_titan,
        ember_spirit,
        enchantress,
        enigma,
        faceless_void,
        grimstroke,
        gyrocopter,
        huskar,
        invoker,
        io,
        jakiro,
        juggernaut,
        keeper_of_the_light,
        kunkka,
        legion_commander,
        leshrac,
        lich,
        lifestealer,
        lina,
        lion,
        lone_druid,
        luna,
        lycan,
        magnus,
        mars,
        medusa,
        meepo,
        mirana,
        monkey_king,
        morphling,
        muerta,
        naga_siren,
        natures_prophet,
        necrophos,
        night_stalker,
        nyx_assassin,
        ogre_magi,
        omniknight,
        oracle,
        outworld_destroyer,
        pangolier,
        phantom_assassin,
        phantom_lancer,
        phoenix,
        puck,
        pudge,
        pugna,
        queen_of_pain,
        razor,
        riki,
        rubick,
        sand_king,
        shadow_demon,
        shadow_fiend,
        shadow_shaman,
        silencer,
        skywrath_mage,
        slardar,
        slark,
        snapfire,
        sniper,
        spectre,
        spirit_breaker,
        storm_spirit,
        sven,
        techies,
        templar_assassin,
        terrorblade,
        tidehunter,
        timbersaw,
        tinker,
        tiny,
        treant_protector,
        troll_warlord,
        tusk,
        underlord,
        undying,
        ursa,
        vengeful_spirit,
        venomancer,
        viper,
        visage,
        void_spirit,
        warlock,
        weaver,
        windranger,
        winter_wyvern,
        witch_doctor,
        wraith_king,
        zeus
    }
}
