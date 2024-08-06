// ==UserScript==
// @name        Helpers for https://learning.oreilly.com
// @namespace   Violentmonkey Scripts
// @match       https://learning.oreilly.com/library/view/pro-c-10/9781484278697/html/*
// @grant       none
// @version     1.0
// @author      letientai299
// ==/UserScript==

function loadHighlightJS() {
  const css = document.createElement("link");
  css.rel = "stylesheet";
  css.href =
    "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css";
  document.head.appendChild(css);

  const hljs = document.createElement("script");
  hljs.src =
    "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js";
  document.head.appendChild(hljs);
}

function highlighting() {
  const codes = [...document.getElementsByClassName("ProgramCode")];

  codes.forEach((div) => {
    const pre = makePreCode(div.innerText);
    div.parentNode.replaceChild(pre, div);
    hljs.highlightElement(pre);
  });
}

function makePreCode(txt) {
  const code = document.createElement("code");
  code.className = "language-csharp";
  txt = txt.replace(/[^\S\n\t]{2}/g, "    ");
  code.textContent = `\r${txt}`;

  const pre = document.createElement("pre");
  pre.appendChild(code);
  return pre;
}

loadHighlightJS();
setTimeout(highlighting, 4000);
