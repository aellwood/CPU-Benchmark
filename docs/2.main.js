(window.webpackJsonp=window.webpackJsonp||[]).push([[2],{11:function(e,n,t){"use strict";t.d(n,"b",(function(){return l})),t.d(n,"a",(function(){return u}));const c="https://raw.githubusercontent.com/",r="L3tum",o="CPU-Benchmark-Database",s="master",a="saves",i="aggregations";function l(e){return`${c}${r}/${o}/${s}/${a}/${e}.json`}function u(e){return`${c}${r}/${o}/${s}/${i}/pagination/${e}.json`}},9:function(e,n,t){"use strict";t.r(n),t.d(n,"render",(function(){return u}));var c=t(0),r=t.n(c),o=t(11);const s=[];let a=1;const i='\n<div class="col-12">\n<div class="card">\n    <a id="heading-[number]" class="detail-button [vendor]" href="[filename]">\n        <div class="card-header d-flex justify-content-between">\n            <h5 class="mb-0">\n                [cpu]\n            </h5>\n            <h5>\n                Score [score] <span class="score [score-class]" title="Faster (green) or slower (orange) than the reference 3900X"></span>\n            </h5>\n        </div>\n    </a>\n </div>\n </div>\n';function l(e=a-1){return new Promise((n,t)=>{s.length>10*e?n(s.slice(10*e,10*e+10)):fetch(Object(o.a)(e+1)).then(t=>{t.ok&&404!==t.status?t.json().then(t=>{s.push(...t.ResultSaveFiles),s.length>10*e?n(s.slice(10*e,10*e+10)):n([])}):n([])})})}function u(){const e=r()("#prev"),n=r()("#next"),t=r()("#current");e.off("click"),e.prop("disabled",!0),n.off("click"),n.prop("disabled",!0),t.html(a),l().then(c=>{r()("#accordion").html(""),function(e){const n=[];let t=[];return e.forEach((function(e,c){n.push(fetch(Object(o.b)(e)).then((function(n){return n.json().then((function(n){let r=i.replace(/\[number]/g,c.toString()).replace(/\[cpu]/g,n.MachineInformation.Cpu.Name).replace(/\[vendor]/g,n.MachineInformation.Cpu.Vendor).replace(/\[filename]/g,"?detail="+e),o=Object.keys(n.Results),s=0,a=0;o.forEach((function(e){if(!Array.isArray(n.Results[e]))return;let t=0,c=0;n.Results[e].forEach((function(e){e.Benchmark.startsWith("Category:")||(t+=e.Points,c++)})),s+=t,a+=c}));const l=s/a;r=r.replace(/\[score]/g,isNaN(l)?"0".padStart(5,"0"):l.toFixed(0).padStart(5,"0")),r=r.replace(/\[score-class]/g,isNaN(l)?"below":l<5e4?"below":5e4===l?"same":"above"),t[c]=r}))})))})),Promise.all(n).then(()=>t.join(""))}(c).then(c=>{r()("#accordion").html(c),r()(".collapse").collapse(),t.html(a),l(a-2).then(n=>{0===n.length?(e.prop("disabled",!0),e.off("click")):(e.prop("disabled",!1),e.on("click",()=>{a--,u()}))}),l(a).then(e=>{0===e.length?(n.prop("disabled",!0),n.off("click")):(n.prop("disabled",!1),n.on("click",()=>{a++,u()}))})})})}}}]);