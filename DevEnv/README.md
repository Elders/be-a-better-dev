Before you start working make sure you configured git properly.

You need to set the core.autocrlf attribute to true on Windows and to input on Linux and OS X. 
You can change the configuration manually by running `git config --global core.autocrlf true` on Windows or `git config --global core.autocrlf input` on Linux and OS X.


If you already have mixed line endings you can fix it following this article:  
https://help.github.com/articles/dealing-with-line-endings/






Bellow is a full .gitconfig file using kdiff3 as merging tool on Windows including helper aliases

```  
[merge]
    tool = kdiff3
[mergetool "kdiff3"]
    path = C:/Program Files/KDiff3/kdiff3.exe
[diff]
    guitool = kdiff3
    algorithm = patience
[difftool "kdiff3"]
    path = C:/Program Files/KDiff3/kdiff3.exe
    cmd = \"C:/Program Files/KDiff3/kdiff3.exe\" \"$LOCAL\" \"$REMOTE\"
[core]
    editor = \"C:/Program Files (x86)/GitExtensions/GitExtensions.exe\" fileeditor
    autocrlf = true
[user]
    name = my-username-here
    email = my-email-here
[mergetool]
    keepBackup = false
[alias]
    # xxx : clean the repo and all subdirs by removing all untracked files
    xxx = "!f() { find . -name .git -type d -execdir git clean -xdf \\; ; }; f"

    # amend 
    amend = commit -a --amend

    # co : checkout
    co = checkout

    # cob : checkout with branch creation if not exist
    cob = checkout -b

    # s : short status
    s = status -sb

    # l : beautiful log
    l = log --graph --pretty=format:\"%C(yellow)%h%C(cyan)%d%Creset %s %C(white)- %an, %ar%Creset\"

    # ls: log with commited files
    ls = log --pretty=format:\"%C(yellow)%h%C(cyan)%d%C(green) %s %C(white)- %an, %ar%Creset\" --decorate --numstat
	
    # d: git diff with word colors
    d = diff --color-words

    # wip : quick save in progress commit
    wip = !git add . && git commit -m \"WIP\"

    # cm : quick commit
    cm = "!git add . && git commit -m "

    # undo : removes last local commit
    undo = reset HEAD~1 --mixed

    # save : quick check point to not loose something
    save = !git add -A && git commit -m 'SAVEPOINT'

    # wipe : save a commit point and undo last change
    wipe = !git add -A && git commit -qm \"UNDO SAVEPOINT\" && git reset HEAD~1 --hard

    # bclean : used by bdone
    bclean = "!f() { git branch --merged ${1-master} | grep -v ${1-master}$ | xargs -r git branch -d; }; f"

    # bdone : chekout master and clean the merged branches
    bdone = "!f() { git checkout ${1-master} && git up && git bclean ${1-master}; }; f"

    # la : list of aliases
    la = !git config -l | grep alias | cut -c 7-

    # b : list the local branches
    b = !git for-each-ref --sort=\"-authordate\" --format=\"%(authordate)%09%(objectname:short)%09%(refname)\" refs/heads | sed -e \"s-refs/heads/--\"

    # ready : rebase interactive
    ready = rebase -i master

    # ready-root : rebase interactive including the root commit
    ready-root = rebase -i --root

    # up : sync master
    up = !git pull --rebase --prune $@ && git submodule update --init --recursive

    # bsync : sync with origin master while staying in your feature branch
    bsync = !git pull --rebase=preserve --prune origin master && git submodule update --init --recursive

    # Add a remote upstream
    upstream = "!f() { git remote add upstream https://github.com/$1.git; }; f"

    # Sync upstream
    syncu = "!f() { git checkout ${1-master} && git fetch upstream && git rebase upstream/master; }; f"

    # See object
    type = cat-file -t
    dump = cat-file -p

    # Push version
    pv = !git push --tag && git push

    # pushit : pushes the chancges to the remote
    pushit = "!f() { git rev-parse --abbrev-ref HEAD ; }; git push --set-upstream origin `f`"

    # Remote	
    r = remote -v

    # beta test
    fixup = !sh -c 'git commit --fixup=$1' -
    squash = !sh -c 'git commit --squash=$1' -
	
    # Pull request locally : git pr 14 (#14 is the id of the pull request)
    pr = "!f() { git fetch -fu ${2:-origin} refs/pull/$1/head:pr-$1 && git checkout pr-$1; }; f"
    pr-clean = "!git for-each-ref refs/heads/pr-* --format=\"%(refname)\" | while read ref ; do branch=${ref#refs/heads/} ; git branch -D $branch ; done"  
```