﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Utils;

public class SeqAnim {
    public string name;
    public float value;
    public float timestamp;
    public Action cb = () => { };
    public Action update = () => { };
    public bool hit = false;

}

public class Sequencer {
    //ordered list of values and timestamps
    //public List callbacks
    public Dictionary<string,List<SeqAnim>> anims = new Dictionary<string, List<SeqAnim>>();
    public float current = 0;
    public float time;
    public SeqAnim last;


    public Sequencer Anim(float val, float time, string name) {
        if(anims.ContainsKey(name) == false) {
            anims[name] = new List<SeqAnim>();
        }
         
        anims[name].Add(new SeqAnim() {
            name = name,
            value = val,
            timestamp = current + time,
        });
        last = anims[name].Last();
        return this;
    }

    public Sequencer Show(float time, string name) {
        Anim(0,0,name);
        Anim(1,time,name);
        return this;
    }


    public Sequencer Hide(float time, string name) {
        Anim(1, 0, name);
        Anim(0, time, name);
        return this;
    }

    //public Sequencer Listen(string name, string type, Action cb) {
    //    anims["_cbevent"].Add(new SeqAnim() {
    //        name = "_cbevent",
    //        timestamp = current,
    //        cb = cb,
    //    });
    //    return this;
    //}

    public void Listen(Action cb) {
        last.cb = cb;
    }

    public void OnUpdate(Action cb) {
        last.update = cb;
    }

    //wait before starting the next animation
    public Sequencer Pause(float time) {
        current += time;
        return this;
    }

    //wait for current animation to finish then continue with remaining animations
    public Sequencer Wait() {
        current = last.timestamp;
        return this;
    }

    public Sequencer WaitAndPause(float time) {
        Wait();
        Pause(time);
        return this;
    }

    //multiple current pointers adding to the same anims list
    public Sequencer Branch() {
        var seq = new Sequencer();
        seq.current = current;
        seq.anims = anims;
        return seq;
    }

    public float Seek(float time, string name) {
        var vals = anims[name].Where(a => a.name == name).ToList();
        
        if(time < vals.First().timestamp) {
            return vals.First().value;
        }

        if(time > vals.Last().timestamp) {
            return vals.Last().value;
        }

        for(int i = 0; i < vals.Count; i++) {

            var a = vals[i];
            var b = vals[i + 1];
            if (inRange(time, a.timestamp, b.timestamp)) {
                return map(time, a.timestamp, b.timestamp, a.value, b.value);
            }
        }

        return 0;
    }


    public float Curve(float x, float a, float b) {
        return (a + b - 2) * x * x * x + (3 - 2 * a - b) * x * x + a * x;
    }

    public void Update(float dt) {


        time += dt;
        foreach (var pair in anims) {

            if (inRange(time, pair.Value.First().timestamp, pair.Value.Last().timestamp)) {
                pair.Value.Last().update();
            }
            
            
            foreach (var anim in pair.Value.Where(a => a.hit == false && time <= a.timestamp)) {
                anim.hit = true;
                anim.cb();
            }
        }

        


        
    }


}
