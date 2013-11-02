using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTorrentPostDownloadScript
{
    /*

    Add this script to Preferences/Advances/Run Program:Run this program when a torrent finishes

    scriptcs PostDownload.csx -- -f %F -d %D -n %N -p %P -l %L -t %T -m %m -i %I -s %S -k %K

    UTorrent docs...

    You can use the following parameters:

    %F - Name of downloaded file (for single file torrents)
    %D - Directory where files are saved
    %N - Title of torrent
    %P - Previous state of torrent
    %L - Label
    %T - Tracker
    %M - Status message string (same as status column)
    %I - hex encoded info-hash
    %S - State of torrent
    %K - kind of torrent (single|multi)

    Where State is one of:

    Error - 1
    Checked - 2
    Paused - 3
    Super seeding - 4
    Seeding - 5
    Downloading - 6
    Super seed [F] - 7
    Seeding [F] - 8
    Downloading [F] - 9
    Queued seed - 10
    Finished - 11
    Queued - 12
    Stopped - 13*/

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
